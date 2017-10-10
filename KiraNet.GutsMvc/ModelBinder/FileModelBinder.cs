using KiraNet.GutsMvc.Implement;
using System;

namespace KiraNet.GutsMvc.ModelBinder
{
    public class FileModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var file = controllerContext.HttpContext.Request.Form.Files.GetFile(bindingContext.ModelName);

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
