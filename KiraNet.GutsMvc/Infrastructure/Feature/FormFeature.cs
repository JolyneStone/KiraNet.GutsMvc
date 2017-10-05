using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using KiraNet.GutsMVC.Helper;

namespace KiraNet.GutsMVC
{
    public class FormFeature : IFormFeature
    {
        private static readonly FormOptions DefaultFormOptions = new FormOptions();

        private readonly HttpRequest _request;
        private readonly FormOptions _options;
        //private Task<IFormCollection> _parsedFormTask;
        private IFormCollection _form;

        public FormFeature(IFormCollection form)
        {
            Form = form ?? throw new ArgumentNullException(nameof(form));
        }
        public FormFeature(HttpRequest request)
            : this(request, DefaultFormOptions)
        {
        }

        public FormFeature(HttpRequest request, FormOptions options)
        {
            _request = request ?? throw new ArgumentNullException(nameof(request));
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }

        private MediaTypeHeaderValue ContentType
        {
            get
            {
                MediaTypeHeaderValue.TryParse(_request.ContentType, out MediaTypeHeaderValue mt);
                return mt;
            }
        }

        public bool HasFormContentType
        {
            get
            {
                if (Form != null)
                {
                    return true;
                }

                var contentType = ContentType;
                return HasApplicationFormContentType(contentType) || HasMultipartFormContentType(contentType);
            }
        }

        public IFormCollection Form
        {
            get { return _form; }
            set
            {
                //_parsedFormTask = null;
                _form = value;
            }
        }

        public IFormCollection ReadForm()
        {
            if (Form != null)
            {
                return Form;
            }

            if (!HasFormContentType)
            {
                throw new InvalidOperationException("Incorrect Content-Type: " + _request.ContentType);
            }

            // 在同步代码中调用异步方法要小心阻塞！！
            return InnerReadForm();
        }

        private IFormCollection InnerReadForm()
        {
            if (!HasFormContentType)
            {
                throw new InvalidOperationException("Incorrect Content-Type: " + _request.ContentType);
            }

            if (_options.BufferBody)
            {
                _request.EnableRewind(_options.MemoryBufferThreshold, _options.BufferBodyLengthLimit);
            }

            FormCollection formFields = null;
            FormFileCollection files = null;

            // Some of these code paths use StreamReader which does not support cancellation tokens.
            //using (cancellationToken.Register((state) => ((HttpContext)state).Abort(), _request.HttpContext))
            //{
            var contentType = ContentType;
            // Check the content-type
            if (HasApplicationFormContentType(contentType))
            {
                var encoding = FilterEncoding(contentType.Encoding);
                using (var formReader = new FormReader(_request.RequestStream, encoding)
                {
                    ValueCountLimit = _options.ValueCountLimit,
                    KeyLengthLimit = _options.KeyLengthLimit,
                    ValueLengthLimit = _options.ValueLengthLimit,
                })
                {
                    formFields = new FormCollection(formReader.ReadForm());
                }
            }
            else if (HasMultipartFormContentType(contentType))
            {
                var formAccumulator = new KeyMultValuesPair();

                var boundary = GetBoundary(contentType, _options.MultipartBoundaryLengthLimit);
                var multipartReader = new MultipartReader(boundary, _request.RequestStream)
                {
                    HeadersCountLimit = _options.MultipartHeadersCountLimit,
                    HeadersLengthLimit = _options.MultipartHeadersLengthLimit,
                    BodyLengthLimit = _options.MultipartBodyLengthLimit,
                };
                var section = multipartReader.ReadNextSection();
                while (section != null)
                {
                    // Parse the content disposition here and pass it further to avoid reparsings
                    ContentDispositionHeaderValue.TryParse(section.ContentDisposition, out ContentDispositionHeaderValue contentDisposition);

                    if (contentDisposition.IsFileDisposition())
                    {
                        var fileSection = new FileMultipartSection(section, contentDisposition);

                        // Enable buffering for the file if not already done for the full body
                        section.EnableRewind(
                            _request.HttpContext.Response.RegisterForDispose,
                            _options.MemoryBufferThreshold, _options.MultipartBodyLengthLimit);

                        // Find the end
                        section.Body.Drain();

                        var name = fileSection.Name;
                        var fileName = fileSection.FileName;

                        FormFile file;
                        if (section.BaseStreamOffset.HasValue)
                        {
                            // Relative reference to buffered request body
                            file = new FormFile(_request.RequestStream, section.BaseStreamOffset.Value, section.Body.Length, name, fileName);
                        }
                        else
                        {
                            // Individually buffered file body
                            file = new FormFile(section.Body, 0, section.Body.Length, name, fileName);
                        }
                        file.Headers = new NameValueCollection().Create(section.Headers.Select(x=>new KeyValuePair<string, string>(x.Key, x.Value.ContvertToString())));

                        if (files == null)
                        {
                            files = new FormFileCollection();
                        }
                        if (files.Count >= _options.ValueCountLimit)
                        {
                            throw new InvalidDataException($"Form value count limit {_options.ValueCountLimit} exceeded.");
                        }
                        files.Add(file);
                    }
                    else if (contentDisposition.IsFormDisposition())
                    {
                        var formDataSection = new FormMultipartSection(section, contentDisposition);

                        // Content-Disposition: form-data; name="key"
                        //
                        // value

                        // Do not limit the key name length here because the mulipart headers length limit is already in effect.
                        var key = formDataSection.Name;
                        var value = formDataSection.GetValue();

                        formAccumulator.Append(key, value);
                        if (formAccumulator.ValueCount > _options.ValueCountLimit)
                        {
                            throw new InvalidDataException($"Form value count limit {_options.ValueCountLimit} exceeded.");
                        }
                    }
                    else
                    {
                        System.Diagnostics.Debug.Assert(false, "Unrecognized content-disposition for this section: " + section.ContentDisposition);
                    }

                    section = multipartReader.ReadNextSection();
                }

                if (formAccumulator.HasValues)
                {
                    formFields = new FormCollection(formAccumulator.GetResults(), files);
                }
            }
            //}

            // Rewind so later readers don't have to.
            if (_request.RequestStream.CanSeek)
            {
                _request.RequestStream.Seek(0, SeekOrigin.Begin);
            }

            if (formFields != null)
            {
                Form = formFields;
            }
            else if (files != null)
            {
                Form = new FormCollection(null, files);
            }
            else
            {
                Form = FormCollection.Empty;
            }

            return Form;
        }

        private Encoding FilterEncoding(Encoding encoding)
        {
            // UTF-7 is insecure and should not be honored. UTF-8 will succeed for most cases.
            if (encoding == null || Encoding.UTF7.Equals(encoding))
            {
                return Encoding.UTF8;
            }
            return encoding;
        }

        private bool HasApplicationFormContentType(MediaTypeHeaderValue contentType)
        {
            // Content-Type: application/x-www-form-urlencoded; charset=utf-8
            return contentType != null && contentType.MediaType.Equals("application/x-www-form-urlencoded", StringComparison.OrdinalIgnoreCase);
        }

        private bool HasMultipartFormContentType(MediaTypeHeaderValue contentType)
        {
            // Content-Type: multipart/form-data; boundary=----WebKitFormBoundarymx2fSWqWSd0OxQqq
            return contentType != null && contentType.MediaType.Equals("multipart/form-data", StringComparison.OrdinalIgnoreCase);
        }

        private bool HasFormDataContentDisposition(ContentDispositionHeaderValue contentDisposition)
        {
            // Content-Disposition: form-data; name="key";
            return contentDisposition != null && contentDisposition.DispositionType.Equals("form-data")
                && StringSegment.IsNullOrEmpty(contentDisposition.FileName) && StringSegment.IsNullOrEmpty(contentDisposition.FileNameStar);
        }

        private bool HasFileContentDisposition(ContentDispositionHeaderValue contentDisposition)
        {
            // Content-Disposition: form-data; name="myfile1"; filename="Misc 002.jpg"
            return contentDisposition != null && contentDisposition.DispositionType.Equals("form-data")
                && (!StringSegment.IsNullOrEmpty(contentDisposition.FileName) || !StringSegment.IsNullOrEmpty(contentDisposition.FileNameStar));
        }

        // Content-Type: multipart/form-data; boundary="----WebKitFormBoundarymx2fSWqWSd0OxQqq"
        // The spec says 70 characters is a reasonable limit.
        private static string GetBoundary(MediaTypeHeaderValue contentType, int lengthLimit)
        {
            var boundary = HeaderUtilities.RemoveQuotes(contentType.Boundary);
            if (StringSegment.IsNullOrEmpty(boundary))
            {
                throw new InvalidDataException("Missing content-type boundary.");
            }
            if (boundary.Length > lengthLimit)
            {
                throw new InvalidDataException($"Multipart boundary length limit {lengthLimit} exceeded.");
            }
            return boundary.ToString();
        }
    }
}
