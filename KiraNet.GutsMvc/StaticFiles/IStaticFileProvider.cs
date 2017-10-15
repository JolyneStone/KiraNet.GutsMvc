namespace KiraNet.GutsMvc.StaticFiles
{
    internal interface IStaticFileProvider
    {
        bool TryGetFileStream(string path, out string content);
    }
}
