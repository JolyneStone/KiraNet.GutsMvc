using System;
using System.IO;
using KiraNet.GutsMvc.Infrastructure;

namespace KiraNet.GutsMvc.View
{
    public class ViewPath
    {
        private static string _viewPath;
        public static string Path
        {
            get
            {
                if (String.IsNullOrWhiteSpace(_viewPath))
                {
                    _viewPath = System.IO.Path.Combine(RootConfiguration.Root, "Views");
                }

                return _viewPath;
            }
        }

        public ViewPath(string viewPath)
        {
            if (viewPath == null && viewPath.Length == 0)
            {
                throw new ArgumentNullException(nameof(viewPath));
            }

            _viewPath = System.IO.Path.Combine(viewPath, "Views");
        }
    }
}
