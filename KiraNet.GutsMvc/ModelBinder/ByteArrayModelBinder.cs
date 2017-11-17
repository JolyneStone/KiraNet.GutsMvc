using KiraNet.GutsMvc.Infrastructure;
using System;

namespace KiraNet.GutsMvc.ModelBinder
{
    /// <summary>
    /// 在默认情况下，如果目标数据类型是一个字节数组（byte[]），则使用ByteArrayModelBinder进行Model绑定
    /// </summary>
    public class ByteArrayModelBinder : IModelBinder
    {
        public virtual object BindModel(HttpContext httpContext, ModelBindingContext bindingContext)
        {
            ValueProviderResult result = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            string attemptedValue = result.StrRawValue;

            return Convert.FromBase64String(attemptedValue.Replace("\"", String.Empty));
        }
    }
}
