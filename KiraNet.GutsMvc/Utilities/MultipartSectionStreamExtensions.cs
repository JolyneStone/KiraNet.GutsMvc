using Microsoft.Net.Http.Headers;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace KiraNet.GutsMvc
{
    public static class MultipartSectionStreamExtensions
    {
        /// <summary>
        /// Reads the body of the section as a string
        /// </summary>
        /// <param name="section">The section to read from</param>
        /// <returns>The body steam as string</returns>
        public static async Task<string> ReadAsStringAsync(this MultipartSection section)
        {
            if (section == null)
            {
                throw new ArgumentNullException(nameof(section));
            }

            MediaTypeHeaderValue.TryParse(section.ContentType, out MediaTypeHeaderValue sectionMediaType);

            Encoding streamEncoding = sectionMediaType?.Encoding;
            if (streamEncoding == null || streamEncoding == Encoding.UTF7)
            {
                streamEncoding = Encoding.UTF8;
            }

            using (var reader = new StreamReader(
                section.Body,
                streamEncoding,
                detectEncodingFromByteOrderMarks: true,
                bufferSize: 1024,
                leaveOpen: true))
            {
                return await reader.ReadToEndAsync();
            }
        }

        public static string ReadAsString(this MultipartSection section)
        {
            if (section == null)
            {
                throw new ArgumentNullException(nameof(section));
            }

            MediaTypeHeaderValue.TryParse(section.ContentType, out MediaTypeHeaderValue sectionMediaType);

            Encoding streamEncoding = sectionMediaType?.Encoding;
            if (streamEncoding == null || streamEncoding == Encoding.UTF7)
            {
                streamEncoding = Encoding.UTF8;
            }

            using (var reader = new StreamReader(
                section.Body,
                streamEncoding,
                detectEncodingFromByteOrderMarks: true,
                bufferSize: 1024,
                leaveOpen: true))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
