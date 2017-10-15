using System;
using System.IO;
using System.Text;

namespace KiraNet.GutsMvc.StaticFiles
{
    internal class StaticFileProvider : IStaticFileProvider
    {
        public bool TryGetFileStream(string path, out string content)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException(nameof(path));
            }

            var file = new FileInfo(path);
            if(!file.Exists)
            {
                content = null;
                return false;
            }

            try
            {
                using (var reader = new StreamReader(file.OpenRead(), Encoding.UTF8))
                {
                    content = reader.ReadToEnd();
                }
                return true;
            }
            catch
            {
                content = null;
                return false;
            }
        }
    }
}
