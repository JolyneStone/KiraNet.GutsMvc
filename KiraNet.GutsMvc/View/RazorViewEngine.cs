namespace KiraNet.GutsMvc.View
{
    public class RazorViewEngine : ViewEngine
    {
        public override IView CreateView(string folderName, string viewName)
        {
            if (string.IsNullOrWhiteSpace(folderName))
            {
                throw new System.ArgumentException("message", nameof(folderName));
            }

            if (string.IsNullOrWhiteSpace(viewName))
            {
                throw new System.ArgumentException("message", nameof(viewName));
            }

            return new RazorView(folderName, viewName);
        }
    }
}