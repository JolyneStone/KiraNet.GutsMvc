namespace KiraNet.GutsMVC.View
{
    public interface IPageProvider
    {
        string CompilePage(string viewName, ViewContext viewContext);
    }
}
