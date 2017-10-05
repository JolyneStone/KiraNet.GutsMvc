using System;
using System.IO;
using System.Text;

namespace KiraNet.GutsMVC.View
{
    public class NVelocityView : IView
    {
        private IPageProvider _pageProvider;
        public NVelocityView(string folderName, string viewName)
        {
            if (string.IsNullOrWhiteSpace(folderName))
            {
                throw new ArgumentException("message", nameof(folderName));
            }

            if (string.IsNullOrWhiteSpace(viewName))
            {
                throw new ArgumentException("message", nameof(viewName));
            }

            FolderName = folderName;
            ViewName = viewName;
            _pageProvider = new NVelocityPageProvider(folderName);
        }

        public string FolderName { get; }
        public string ViewName { get; }


        public async void Render(ViewContext viewContext)
        {
            var html = _pageProvider.CompilePage(ViewName, viewContext);


            //var stream = new StreamWriter(viewContext.HttpContext.Response.ResponseStream, Encoding.UTF8);
            //await stream.WriteAsync(html);
            var buffer = Encoding.UTF8.GetBytes(html);
            await viewContext.HttpContext.Response.ResponseStream.WriteAsync(buffer, 0, buffer.Length);
        }
    }
}
