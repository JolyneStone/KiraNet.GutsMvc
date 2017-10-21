using KiraNet.GutsMvc.Helper;
using System;
using System.Threading.Tasks;

namespace KiraNet.GutsMvc.View
{
    internal class RazorView : IView
    {
        private RazorTemplateProvider _templateProvider;
        private RazorPageViewBase _razorInstance;
        private KiraSpinLock _lock = new KiraSpinLock();
        public RazorView(string folderName, string viewName)
        {
            if (string.IsNullOrWhiteSpace(folderName))
            {
                throw new ArgumentException(nameof(folderName));
            }

            if (string.IsNullOrWhiteSpace(viewName))
            {
                throw new ArgumentException(nameof(viewName));
            }

            FolderName = folderName;
            ViewName = viewName;
            _templateProvider = new RazorTemplateProvider(folderName);
        }

        public string FolderName { get; }
        public string ViewName { get; }

        public void Render(ViewContext viewContext)
        {
            GetView(viewContext);
            _razorInstance.ExecuteViewAsync(viewContext, null).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        public async Task RenderAsync(ViewContext viewContext)
        {
            GetView(viewContext);
            await _razorInstance.ExecuteViewAsync(viewContext, null);
        }

        private void GetView(ViewContext viewContext)
        {
            if (_razorInstance == null)
            {
                var razorInstance = _templateProvider.CompileTemplate(ViewName, viewContext).GetAwaiter().GetResult();

                _lock.Enter();
                _razorInstance = razorInstance;
                _lock.Exit();
            }
        }
    }
}