using System;
using System.Collections.Generic;

namespace KiraNet.GutsMVC.Metadata
{
    public interface IModelMetadataProvider
    {
        /// <summary>
        /// 获取指定容器的所有属性元数据
        /// </summary>
        /// <param name="container"></param>
        /// <param name="containerType"></param>
        /// <returns></returns>
        IEnumerable<ModelMetadata> GetMetadataForProperties(object container, Type containerType);

        /// <summary>
        /// 获取指定容器的某个指定属性元数据
        /// </summary>
        /// <param name="modelAccessor"></param>
        /// <param name="containerType"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        ModelMetadata GetMetadataForProperty(Func<object> modelAccessor, Type containerType, string propertyName);

        /// <summary>
        /// 获取指定容器对象的元数据
        /// </summary>
        /// <param name="modelAccessor"></param>
        /// <param name="modelType"></param>
        /// <returns></returns>
        ModelMetadata GetMetadataForType(Func<object> modelAccessor, Type modelType);
    }
}