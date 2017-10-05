using KiraNet.GutsMVC.Helper;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace KiraNet.GutsMVC.Metadata
{
    //while(true)
    //{
    //    double x = out.NextDouble();
    //    if(x<10||x>20)
    //    {
    //        continue;
    //    }
    //    else
    //    {
    //        ...........
    //    }
    //}



    /// <summary>
    /// 根据应用在数据类型或其数据成员上关联的特性来解析Model元数据
    /// </summary>
    public abstract class ModelMetadataProvider : IModelMetadataProvider
    {
        private string _cacheKeyPrefix;
        private static KiraSpinLock _lock = new KiraSpinLock();
        private static ConcurrentDictionary<Type, string> _typeIds = new ConcurrentDictionary<Type, string>();
        /// <summary>
        /// 用于缓存Model元数据
        /// </summary>
        private IMemoryCache _prototypeCache;
        public ModelMetadataProvider(IMemoryCache cache)
        {
            _prototypeCache = cache;
        }


        public static ModelMetadataProvider Current { get; protected set; }

        protected string CacheKeyPrefix
        {
            get
            {
                if (_cacheKeyPrefix == null)
                {
                    _cacheKeyPrefix = "MetadataPrototypes::" + GetType().GUID.ToString("B");
                }

                return _cacheKeyPrefix;
            }
        }

        protected virtual ModelMetadata CreateMetadata(
            IEnumerable<Attribute> attributes,
            Type containerType, 
            Func<object> modelAccessor,
            Type modelType, 
            string propertyName)
        {
            // 首先根据数据类型和属性名称生成一个key，并从缓存中获取预先创建的ModelMetadata对象
            // 如果该ModelMetadata对象存在，则调用抽象方法CreateMetadataFromPrototype方法，以此对象为原型创建一个新的ModelMetadata对象
            // 否则，调用抽象方法CreateMetadataPrototype方法以创建作为原型的ModelMetadata对象，并缓存之

            Type typeForCache = containerType ?? modelType;
            string cacheKey = GetCacheKey(typeForCache, propertyName);
            _prototypeCache.TryGetValue<ModelMetadata>(cacheKey, out ModelMetadata prototype);

            if (prototype == null)
            {
                prototype = CreateMetadataPrototype(
                    attributes,
                    typeForCache,
                    modelType,
                    propertyName);

                if (prototype != null)
                {
                    _lock.Enter();
                    _prototypeCache.Set(
                        cacheKey,
                        prototype,
                        new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(20)));
                    _lock.Exit();
                }
                else
                {
                    return null;
                }
            }

            return CreateMetadataFromPrototype(prototype, modelAccessor);
        }

        public virtual IEnumerable<ModelMetadata> GetMetadataForProperties(object container, Type containerType)
        {
            if (container == null)
            {
                throw new ArgumentNullException(nameof(container));
            }

            if (containerType == null)
            {
                throw new ArgumentNullException(nameof(containerType));
            }

            return containerType.GetProperties()
                .Select(property =>
                {
                    Func<object> propertyAccessor = container == null ?
                        null :
                        (Func<object>)(() => property.GetValue(container)); // 不知为何，这里直接写lamada表达式会显示没有隐式转换
                    return GetMetadataForProperty(propertyAccessor, containerType, property);
                });
        }

        public virtual ModelMetadata GetMetadataForProperty(Func<object> modelAccessor, Type containerType, string propertyName)
        {
            if (containerType == null)
            {
                throw new ArgumentNullException(nameof(containerType));
            }

            if (String.IsNullOrWhiteSpace(propertyName))
            {
                throw new ArgumentNullException(nameof(propertyName));
            }

            var attributes = containerType.GetCustomAttributes(true).OfType<Attribute>();

            var property = containerType.GetProperty(
                propertyName,
                BindingFlags.IgnoreCase |
                BindingFlags.Instance |
                BindingFlags.Public);

            if (property == null)
            {
                throw new ArgumentException($"找不到指定的属性：{propertyName}");
            }

            return GetMetadataForProperty(modelAccessor, containerType, property);
        }

        protected virtual ModelMetadata GetMetadataForProperty(Func<object> modelAccessor, Type containerType, PropertyInfo property)
        {
            var attributes = property.GetCustomAttributes(true).OfType<Attribute>();
            ModelMetadata metadata = CreateMetadata(attributes, containerType, modelAccessor, property.PropertyType, property.Name);

            ApplyMetadataAwareAttributes(attributes, metadata);

            return metadata;
        }

        public virtual ModelMetadata GetMetadataForType(Func<object> modelAccessor, Type modelType)
        {
            if (modelType == null)
            {
                throw new ArgumentNullException(nameof(modelType));
            }

            var attributes = modelType.GetCustomAttributes(true).OfType<Attribute>();
            ModelMetadata metadata = CreateMetadata(attributes, null, modelAccessor, modelType, null);

            ApplyMetadataAwareAttributes(attributes, metadata);

            return metadata;
        }

        /// <summary>
        /// 筛选出继承了IMetadataAware接口的特性，并应用它
        /// </summary>
        /// <param name="attributes"></param>
        /// <param name="metadata"></param>
        private void ApplyMetadataAwareAttributes(IEnumerable<Attribute> attributes, ModelMetadata metadata)
        {
            foreach (var metadataAware in attributes.OfType<IMatedataAware>())
            {
                metadataAware.OnMetadataCreated(metadata);
            }
        }

        internal string GetCacheKey(Type type, string propertyName = null)
        {
            propertyName = propertyName ?? String.Empty;
            return CacheKeyPrefix + GetTypeId(type) + propertyName;
        }

        private static string GetTypeId(Type type)
        {
            // B是Guid的一种格式。分别有N、B、D、P四种格式
            return _typeIds.GetOrAdd(type, _ => Guid.NewGuid().ToString("B"));
        }

        /// <summary>
        /// 创建Metadata原型
        /// </summary>
        /// <param name="attributes"></param>
        /// <param name="containerType"></param>
        /// <param name="modelType"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        protected abstract ModelMetadata CreateMetadataPrototype(IEnumerable<Attribute> attributes, Type containerType, Type modelType, string propertyName);

        /// <summary>
        /// 从原型中创建Metadata
        /// </summary>
        /// <param name="prototype"></param>
        /// <param name="modelAccessor"></param>
        /// <returns></returns>
        protected abstract ModelMetadata CreateMetadataFromPrototype(ModelMetadata prototype, Func<object> modelAccessor);
    }
}
