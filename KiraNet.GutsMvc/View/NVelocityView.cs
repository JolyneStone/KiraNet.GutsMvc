using System;
using System.Text;
using System.Threading.Tasks;

namespace KiraNet.GutsMvc.View
{
    public class NVelocityView : IView
    {
        private NVelocityTemplateProvider _templateProvider;
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
            _templateProvider = new NVelocityTemplateProvider(folderName);
        }

        public string FolderName { get; }
        public string ViewName { get; }


        public void Render(ViewContext viewContext)
        {
            var writer = _templateProvider.CompileTemplate(ViewName, viewContext).ConfigureAwait(false).GetAwaiter().GetResult();
            string html = writer.GetStringBuilder()?.ToString();
            if (String.IsNullOrWhiteSpace(html))
            {
                html = String.Empty;
            }

            var buffer = Encoding.UTF8.GetBytes(html);
            viewContext.HttpContext.Response.ResponseStream.Write(buffer, 0, buffer.Length);
        }

        public async Task RenderAsync(ViewContext viewContext)
        {
            var writer = await _templateProvider.CompileTemplate(ViewName, viewContext);
            string html = writer.GetStringBuilder()?.ToString();
            if (String.IsNullOrWhiteSpace(html))
            {
                html = String.Empty;
            }

            var buffer = Encoding.UTF8.GetBytes(html);
            await viewContext.HttpContext.Response.ResponseStream.WriteAsync(buffer, 0, buffer.Length);
        }
    }
}
