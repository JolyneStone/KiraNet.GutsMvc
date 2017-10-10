using KiraNet.GutsMvc.Infrastructure;
using KiraNet.GutsMvc.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KiraNet.GutsMvc.ModelBinder
{
    /// <summary>
    /// 模型绑定上下文对象
    /// </summary>
    public class ModelBindingContext
    {
        //private ModelMetadata _modelMetadata;
        private IDictionary<string, ModelMetadata> _propertyModelMetadata;
        public ModelBindingContext(IValueProvider valueProvider)
        {
            ValueProvider = valueProvider;
        }

        public ModelBindingContext(ModelBindingContext bindingContext)
        {
            if (bindingContext != null)
            {
                ValueProvider = bindingContext.ValueProvider;
            }
        }

        public object Model => ModelMetadata.Model;
        /// <summary>
        /// 获取或设置一个为Model绑定提供原始数据的ValueProvider对象
        /// </summary>
        public IValueProvider ValueProvider { get; set; }
        /// <summary>
        /// ModelName指的是参数名
        /// </summary>
        public string ModelName { get; set; } = String.Empty;
        public Type ModelType => ModelMetadata.ModelType;

        public ModelMetadata ModelMetadata { get; set; }
        public IDictionary<string, ModelMetadata> PropertyMetadata
        {
            get
            {
                if (_propertyModelMetadata == null)
                {
                    _propertyModelMetadata = ModelMetadata.Properties?.ToDictionary(x => x.PropertyName, StringComparer.OrdinalIgnoreCase);
                }

                return _propertyModelMetadata;
            }
        }

    }
}