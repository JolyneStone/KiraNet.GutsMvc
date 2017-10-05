using System;

namespace KiraNet.GutsMVC.View
{
    internal sealed class ViewCacheKey
    {
        public string ControllerName { get; set; }
        public string ViewName { get; set; }

        public ViewCacheKey(string controllerName, string viewName)
        {
            ControllerName = controllerName ?? String.Empty;
            ViewName = viewName ?? String.Empty;
        }

        public override bool Equals(object obj)
        {
            if (obj is ViewCacheKey key && key != null)
            {
                if (String.Equals(ControllerName, key.ControllerName, StringComparison.OrdinalIgnoreCase) &&
                    String.Equals(ViewName, key.ViewName, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        public override int GetHashCode()
        {
            return ControllerName.ToLower().GetHashCode() ^
                ViewName.ToLower().GetHashCode();
        }
    }
}
