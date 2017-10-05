using System;

namespace KiraNet.GutsMVC.View
{
    public class ViewPath
    {
        private static string _viewPath;
        public static string Path
        {
            get
            {
                if(_viewPath==null)
                {
                    _viewPath = AppContext.BaseDirectory;
                }

                return _viewPath;
            }
        }

        public ViewPath(string viewPath)
        {
            if(viewPath == null && viewPath.Length == 0)
            {
                throw new ArgumentNullException(nameof(viewPath));
            }

            _viewPath = viewPath;
        }
    }
}
