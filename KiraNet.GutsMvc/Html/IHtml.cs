namespace KiraNet.GutsMvc.Html
{
    public interface IHtml
    {
        string PrintHtml(string propertyName, IHtmlGenerator htmlGenerator);
    }
}
