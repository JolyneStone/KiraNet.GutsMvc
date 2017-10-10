using System;

namespace KiraNet.GutsMvc.View
{
    public class NVelocityViewEngine : ViewEngine
    {
        public override IView CreateView(string folderName, string viewName)
        {
            if (string.IsNullOrWhiteSpace(folderName))
            {
                throw new ArgumentException("message", nameof(folderName));
            }

            if (string.IsNullOrWhiteSpace(viewName))
            {
                throw new ArgumentException("message", nameof(viewName));
            }

            return new NVelocityView(folderName, viewName);
        }
    }
}