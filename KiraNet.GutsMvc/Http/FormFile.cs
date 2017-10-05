using System;
using System.Collections.Specialized;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace KiraNet.GutsMVC
{
    public class FormFile : IFormFile
    {
        // Stream.CopyTo()方法的默认缓存大小为80KB
        //private const int _defaultBufferSize = 80 * 1024;
        private readonly Stream _stream;
        private readonly long _streamOffset;

        public FormFile(Stream stream, long streamOffset, long length, string name, string filename)
        {
            _stream = stream;
            _streamOffset = streamOffset;
            Length = length;
            Name = name;
            FileName = filename;
        }

        /// <summary>
        /// 上传的文件长度
        /// </summary>
        public long Length { get; internal set; }

        /// <summary>
        /// Content-Disposition 标头中的名称(可能含有路径)
        /// </summary>
        public string Name { get; internal set; }

        /// <summary>
        /// Content-Disposition 标头中的文件名
        /// </summary>
        public string FileName { get; internal set; }

        public NameValueCollection Headers { get; internal set; }

        /// <summary>
        /// 获取上传文件的Content-Disposition标头
        /// </summary>
        public string ContentDisposition
        {
            get => Headers["Content-Disposition"];
            internal set => Headers["Content-Disposition"] = value;
        }

        /// <summary>
        /// 获取上传文件的Content-Type标头
        /// </summary>
        public string ContentType
        {
            get => Headers["Content-Type"];
            internal set => Headers["Content-Type"] = value;
        }

        /// <summary>
        /// 打开sequest stream
        /// </summary>
        /// <returns></returns>
        public Stream OpenReadStream()
            => new ReferenceReadStream(_stream, _streamOffset, Length);

        /// <summary>
        /// 复制文件内容到指定流中
        /// </summary>
        /// <param name="target"></param>
        public void CopyTo(Stream target)
        {
            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            using (var readStream = OpenReadStream())
            {
                readStream.CopyTo(target);
            }
        }

        /// <summary>
        /// 复制文件内容到指定的流中
        /// </summary>
        /// <param name="target"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task CopyToAsync(Stream target, CancellationToken cancellationToken = new CancellationToken())
        {
            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            using (var readStream = OpenReadStream())
            {
                await readStream.CopyToAsync(target, 80 * 1024, cancellationToken);
            }
        }
    }
}
