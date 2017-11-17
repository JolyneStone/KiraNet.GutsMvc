using System;

namespace KiraNet.GutsMvc.ModelBinder
{
    public class FileModelBinder : IModelBinder
    {
        public object BindModel(HttpContext httpContext, ModelBindingContext bindingContext)
        {
            var file = httpContext.Request.Form.Files.GetFile(bindingContext.ModelName);

            if (file == null ||
                (file.Length == 0 &&
                String.IsNullOrWhiteSpace(file.FileName)))
            {
                return null;
            }

            return file;
        }
    }
}
