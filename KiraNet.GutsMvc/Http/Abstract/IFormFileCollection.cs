using System.Collections.Generic;

namespace KiraNet.GutsMVC
{
    /// <summary>
    /// 表示Request.From.Files的集合
    /// 对IFormFile的只读封装
    /// </summary>
    public interface IFormFileCollection : IReadOnlyList<IFormFile>
    {

        IFormFile this[string name] { get; }

        IFormFile GetFile(string name);

        IReadOnlyList<IFormFile> GetFiles(string name);
    }
}