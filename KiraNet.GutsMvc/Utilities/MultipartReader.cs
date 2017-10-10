using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.IO;
using KiraNet.GutsMvc.Helper;

namespace KiraNet.GutsMvc
{
    public class MultipartReader
    {
        public const int DefaultHeadersCountLimit = 16;
        public const int DefaultHeadersLengthLimit = 1024 * 16;
        private const int DefaultBufferSize = 1024 * 4;

        private readonly BufferedReadStream _stream;
        private readonly MultipartBoundary _boundary;
        private MultipartReaderStream _currentStream;

        public MultipartReader(string boundary, Stream stream)
            : this(boundary, stream, DefaultBufferSize)
        {
        }

        public MultipartReader(string boundary, Stream stream, int bufferSize)
        {
            if (boundary == null)
            {
                throw new ArgumentNullException(nameof(boundary));
            }

            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            if (bufferSize < boundary.Length + 8) // Size of the boundary + leading and trailing CRLF + leading and trailing '--' markers.
            {
                throw new ArgumentOutOfRangeException(nameof(bufferSize), bufferSize, "Insufficient buffer space, the buffer must be larger than the boundary: " + boundary);
            }
            _stream = new BufferedReadStream(stream, bufferSize);
            _boundary = new MultipartBoundary(boundary, false);
            // This stream will drain any preamble data and remove the first boundary marker.
            // HeadersLengthLimit can't be modified until after the constructor.
            _currentStream = new MultipartReaderStream(_stream, _boundary) { LengthLimit = HeadersLengthLimit };
        }

        /// <summary>
        /// The limit for the number of headers to read.
        /// </summary>
        public int HeadersCountLimit { get; set; } = DefaultHeadersCountLimit;

        /// <summary>
        /// The combined size limit for headers per multipart section.
        /// </summary>
        public int HeadersLengthLimit { get; set; } = DefaultHeadersLengthLimit;

        /// <summary>
        /// The optional limit for the total response body length.
        /// </summary>
        public long? BodyLengthLimit { get; set; }

        public MultipartSection ReadNextSection()
        {
            // Drain the prior section.
            _currentStream.Drain();
            // If we're at the end return null
            if (_currentStream.FinalBoundaryFound)
            {
                // There may be trailer data after the last boundary.
                _stream.Drain(HeadersLengthLimit);
                return null;
            }
            var headers = ReadHeaders();
            _boundary.ExpectLeadingCrlf = true;
            _currentStream = new MultipartReaderStream(_stream, _boundary) { LengthLimit = BodyLengthLimit };
            long? baseStreamOffset = _stream.CanSeek ? (long?)_stream.Position : null;
            return new MultipartSection() { Headers = headers, Body = _currentStream, BaseStreamOffset = baseStreamOffset };
        }

        private Dictionary<string, StringValues> ReadHeaders()
        {
            int totalSize = 0;
            var accumulator = new KeyMultValuesPair();
            var line = _stream.ReadLine(HeadersLengthLimit - totalSize);
            while (!string.IsNullOrEmpty(line))
            {
                if (HeadersLengthLimit - totalSize < line.Length)
                {
                    throw new InvalidDataException($"Multipart headers length limit {HeadersLengthLimit} exceeded.");
                }
                totalSize += line.Length;
                int splitIndex = line.IndexOf(':');
                if (splitIndex <= 0)
                {
                    throw new InvalidDataException($"Invalid header line: {line}");
                }

                var name = line.Substring(0, splitIndex);
                var value = line.Substring(splitIndex + 1, line.Length - splitIndex - 1).Trim();
                accumulator.Append(name, value);
                if (accumulator.KeyCount > HeadersCountLimit)
                {
                    throw new InvalidDataException($"Multipart headers count limit {HeadersCountLimit} exceeded.");
                }

                line = _stream.ReadLine(HeadersLengthLimit - totalSize);
            }

            return accumulator.GetResults();
        }
    }
}