using KiraNet.GutsMVC.Implement;
using System;
using System.IO;
using System.Threading.Tasks;

namespace KiraNet.GutsMVC
{
    public class FileResult : IActionResult
    {
        public Stream FileStream { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }

        public void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var response = context.HttpContext.Response;
            response.ContentType = ContentType;
            string fileName = Path.GetFileName(FileName);
            response.Headers.Add("Content-Disposition", "attachment; filename=" + fileName);

            if (FileStream == null)
            {
                if (FileName == null)
                {
                    throw new InvalidOperationException("在Stream为空的情况下，FileName不准为空");
                }
                try
                {
                    using (Stream input = File.OpenRead(FileName))
                    {
                        response.ContentLength64 = input.Length;
                        WriteStream(input, response.ResponseStream);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"有{ex.GetType().Name}异常发生在{FileName}", ex);
                }
            }

            else
            {
                response.ContentLength64 = FileStream.Length;
                try
                {
                    WriteStream(FileStream, response.ResponseStream);
                }
                catch (ArgumentException)
                {
                    // 偏移位超出限定的异常可以不用理它
                }
                finally
                {
                    FileStream.Dispose();
                }
            }
        }

        public async Task ExecuteResultAsync(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var response = context.HttpContext.Response;
            response.ContentType = ContentType;
            string fileName = Path.GetFileName(FileName);
            response.Headers.Add("Content-Disposition", "attachment; filename=" + fileName);

            if (FileStream == null)
            {
                try
                {
                    using (Stream input = File.OpenRead(FileName))
                    {
                        response.ContentLength64 = input.Length;
                        await WriteStreamAsync(input, response.ResponseStream);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"有{ex.GetType().Name}异常发生在{FileName}", ex);
                }
            }
            else
            {
                response.ContentLength64 = FileStream.Length;
                try
                {
                    await WriteStreamAsync(FileStream, response.ResponseStream);
                }
                catch (ArgumentException)
                {
                    // 偏移位超出限定的异常可以不用理它
                }
                finally
                {
                    FileStream.Dispose();
                }
            }
        }

        private void WriteStream(Stream readStream, Stream writeStream)
        {
            // 默认8192个字节的缓存区 在C/C++、C#、Java、PHP等语言中都是如此
            // 对此我也不知道是为什么
            byte[] buffer = new byte[8192];
            int bytesRead;
            while ((bytesRead = readStream.Read(buffer, 0, buffer.Length)) > 0)
            {
                writeStream.Write(buffer, 0, buffer.Length);
            }
        }

        private async Task WriteStreamAsync(Stream readStream, Stream writeStream)
        {
            byte[] buffer = new byte[8192];
            int bytesRead;
            while ((bytesRead = await readStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
            {
                await writeStream.WriteAsync(buffer, 0, buffer.Length);
            }
        }
    }
}
