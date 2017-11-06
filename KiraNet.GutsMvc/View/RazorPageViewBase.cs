using KiraNet.GutsMvc.Helper;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace KiraNet.GutsMvc.View
{
    public abstract class RazorPageViewBase
    {
        private static readonly Encoding UTF8NoBOM = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false, throwOnInvalidBytes: true);

        protected HttpContext Context { get; private set; }

        protected HttpRequest Request { get; private set; }

        protected HttpResponse Response { get; private set; }

        //protected StreamWriter Output { get; private set; }
        protected StringBuilder Output { get; private set; }

        protected HtmlEncoder HtmlEncoder { get; set; } = HtmlEncoder.Default;

        protected UrlEncoder UrlEncoder { get; set; } = UrlEncoder.Default;

        protected ViewDataDictionary ViewData { get; private set; }

        protected TempDataDictionary TempData { get; private set; }

        protected dynamic ViewBag { get; private set; }

        protected string Layout { get; set; } = "_Layout";

        protected JavaScriptEncoder JavaScriptEncoder { get; set; } = JavaScriptEncoder.Default;

        private KiraSpinLock _lock = new KiraSpinLock();
        private Action<StringBuilder> _executeChild;
        private volatile bool _isCanWrite = false;

        public virtual async Task ExecuteViewAsync(ViewContext viewContext, Action<StringBuilder> executeChild = null)
        {
            Output = new StringBuilder();
            Context = viewContext.HttpContext;
            Request = viewContext.HttpContext.Request;
            Response = viewContext.HttpContext.Response;
            ViewData = viewContext.ViewData;
            TempData = viewContext.TempData;
            ViewBag = viewContext.ViewBag;

            if (executeChild != null)
            {
                _lock.Enter();
                _executeChild = executeChild;
                _lock.Exit();
            }

            await ExecuteAsync();

            if (!String.IsNullOrWhiteSpace(Layout))
            {
                // 有布局页方式
                var parentViewContext = new ViewContext(viewContext);
                var cache = viewContext.HttpContext.Service.GetService<IMemoryCache>();
                if (!cache.TryGetValue<RazorPageViewBase>(Layout, out var parentViewBase))
                {
                    parentViewBase = await new RazorTemplateProvider("Shared").CompileTemplate(Layout, parentViewContext);

                    _lock.Enter();
                    cache.Set<RazorPageViewBase>(Layout, parentViewBase,
                        new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(20)));
                    _lock.Exit();
                }

                try
                {
                    await parentViewBase.ExecuteViewAsync(parentViewContext, wirter =>
                    {
                        wirter.Append(Output.ToString());
                    });
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }

                _isCanWrite = false;
            }
            else
            {
                _isCanWrite = true;
            }

            _lock.Enter();
            _executeChild = null;
            _lock.Exit();

            await WriteStream();
        }

        private async Task WriteStream()
        {
            if (_isCanWrite)
            {
                using (var writer = new StreamWriter(Response.ResponseStream, UTF8NoBOM, 4096, leaveOpen: true))
                {
                    await writer.WriteAsync(Output.ToString());
                }
            }
        }

        /// <summary>
        /// 载入子页面
        /// 注：载入子页面的工作在内部完成，因此返回值为String.Empty
        /// </summary>
        public string IncludeBody()
        {
            if (Output != null)
            {
                if (_executeChild != null)
                {
                    _executeChild(Output);

                    _lock.Enter();
                    _executeChild = null;
                    _lock.Exit();
                }
            }

            return String.Empty;
        }

        public abstract Task ExecuteAsync();

        protected void WriteLiteral(string value)
        {
            WriteLiteralTo(Output, value);
        }

        protected void WriteLiteral(object value)
        {
            WriteLiteralTo(Output, value);
        }

        private List<string> AttributeValues { get; set; }

        protected void WriteAttributeValue(string thingy, int startPostion, object value, int endValue, int dealyo, bool yesno)
        {
            if (AttributeValues == null)
            {
                AttributeValues = new List<string>();
            }

            if (value == null)
            {
                return;
            }

            AttributeValues.Add(value.ToString());
        }

        private string AttributeEnding { get; set; }

        protected void BeginWriteAttribute(string name, string begining, int startPosition, string ending, int endPosition, int thingy)
        {
            Output.Append(begining);
            AttributeEnding = ending;
        }

        protected void EndWriteAttribute()
        {
            //var attributes = string.Join(" ", AttributeValues);
            var attributes = string.Join("", AttributeValues);
            Output.Append(attributes);
            AttributeValues = null;

            Output.Append(AttributeEnding);
            AttributeEnding = null;
        }

        protected void WriteAttributeTo(
            StringBuilder writer,
            string name,
            string leader,
            string trailer,
            params AttributeValue[] values)
        {
            if (writer == null)
            {
                throw new ArgumentNullException(nameof(writer));
            }

            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (leader == null)
            {
                throw new ArgumentNullException(nameof(leader));
            }

            if (trailer == null)
            {
                throw new ArgumentNullException(nameof(trailer));
            }


            WriteLiteralTo(writer, leader);
            foreach (var value in values)
            {
                WriteLiteralTo(writer, value.Prefix);

                string stringValue;
                if (value.Value is bool)
                {
                    if ((bool)value.Value)
                    {
                        stringValue = name;
                    }
                    else
                    {
                        continue;
                    }
                }
                else
                {
                    stringValue = value.Value as string;
                }

                if (value.Literal && stringValue != null)
                {
                    WriteLiteralTo(writer, stringValue);
                }
                else if (value.Literal)
                {
                    WriteLiteralTo(writer, value.Value);
                }
                else if (stringValue != null)
                {
                    WriteTo(writer, stringValue);
                }
                else
                {
                    WriteTo(writer, value.Value);
                }
            }

            WriteLiteralTo(writer, trailer);
        }

        protected void Write(object value)
        {
            WriteTo(Output, value);
        }

        protected void Write(string value)
        {
            WriteTo(Output, value);
        }

        protected void Write(HelperResult result)
        {
            WriteTo(Output, result);
        }

        protected void WriteTo(StringBuilder writer, object value)
        {
            if (value != null)
            {
                if (value is HelperResult helperResult)
                {
                    helperResult.WriteTo(writer);
                }
                else
                {
                    WriteTo(writer, Convert.ToString(value, CultureInfo.InvariantCulture));
                }
            }
        }

        protected void WriteTo(StringBuilder writer, string value)
        {
            if (value == null)
            {
                return;
            }

            WriteLiteralTo(writer, HtmlEncoder.Encode(value));
        }

        protected void WriteLiteralTo(StringBuilder writer, object value)
        {
            if (value == null)
            {
                return;
            }

            WriteLiteralTo(writer, Convert.ToString(value, CultureInfo.InvariantCulture));
        }


        protected void WriteLiteralTo(StringBuilder writer, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                writer.Append(value);
            }
        }


        //protected void WriteTo(TextWriter writer, object value)
        //{
        //    if (value != null)
        //    {
        //        if (value is HelperResult helperResult)
        //        {
        //            helperResult.WriteTo(writer);
        //        }
        //        else
        //        {
        //            WriteTo(writer, Convert.ToString(value, CultureInfo.InvariantCulture));
        //        }
        //    }
        //}

        //protected void WriteTo(TextWriter writer, string value)
        //{
        //    WriteLiteralTo(writer, HtmlEncoder.Encode(value));
        //}

        //protected void WriteLiteralTo(TextWriter writer, object value)
        //{
        //    WriteLiteralTo(writer, Convert.ToString(value, CultureInfo.InvariantCulture));
        //}


        //protected void WriteLiteralTo(TextWriter writer, string value)
        //{
        //    if (!string.IsNullOrEmpty(value))
        //    {
        //        writer.Write(value);
        //    }
        //}

        protected string HtmlEncodeAndReplaceLineBreaks(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }

            return string.Join("<br />" + Environment.NewLine,
                input.Split(new[] { "\r\n" }, StringSplitOptions.None)
                .SelectMany(s => s.Split(new[] { '\r', '\n' }, StringSplitOptions.None))
                .Select(HtmlEncoder.Encode));
        }
    }
}
