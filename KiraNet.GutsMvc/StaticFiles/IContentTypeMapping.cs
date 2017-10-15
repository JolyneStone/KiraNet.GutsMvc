namespace KiraNet.GutsMvc.StaticFiles
{
    internal interface IContentTypeMapping
    {
        bool TryGetContentType(string extensionName, out string contentType);
    }
}
