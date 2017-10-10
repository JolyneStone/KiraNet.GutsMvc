using System.Collections.Specialized;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace KiraNet.GutsMvc
{
    /// <summary>
    /// 表示上传的文件及其信息
    /// </summary>
    public interface IFormFile
    {
        string ContentType { get; }
        /// <summary>
        /// Content-disposition 是 MIME 协议的扩展，MIME 协议指示 MIME 用户代理如何显示附加的文件。
        /// 例如，值为attachment时，当 Internet Explorer 接收到头时，它会激活文件下载对话框，它的文件名框自动填充了头中指定的文件名
        /// </summary>
        string ContentDisposition { get; }

        NameValueCollection Headers { get; }

        long Length { get; }

        /// <summary>
        /// 得到Content-Disposition标头指示的名称
        /// 例如：Content-Disposition: attachment; name=“filename.xls”
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 得到Content-Disposition标头指示的文件名
        /// 例如：Content-Disposition: attachment; filename=“filename.xls”
        /// 可能HTTP中指示的文件名携带有路径信息，但一般会被忽略，直接显示最后一部分作为文件名
        /// </summary>
        string FileName { get; }

        Stream OpenReadStream();

        /// <summary>
        /// 复制上传文件的内容到指定的流中
        /// </summary>
        /// <param name="target"></param>
        void CopyTo(Stream target);

        /// <summary>
        /// 异步复制上传文件内容到指定的流中
        /// </summary>
        /// <param name="target"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task CopyToAsync(Stream target, CancellationToken cancellationToken = default(CancellationToken));
    }
}