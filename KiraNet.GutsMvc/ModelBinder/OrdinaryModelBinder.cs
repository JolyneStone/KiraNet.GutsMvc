using KiraNet.GutsMvc.Helper;
using KiraNet.GutsMvc.Implement;
using KiraNet.GutsMvc.Infrastructure;
using KiraNet.GutsMvc.Metadata;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace KiraNet.GutsMvc.ModelBinder
{
    public class OrdinaryModelBinder : IModelBinder
    {
        private ModelBinderDictionary _binders;
        private IModelMetadataProvider _metadataProvider;

        internal ModelBinderDictionary Binders
        {
            get
            {
                if (_binders == null)
                {
                    _binders = ModelBinders.Binders;
                    if (!_binders.ContainsKey(typeof(IModelBinder)))
                    {
                        _binders.Add(typeof(IModelBinder), this);
                    }
                }

                return _binders;
            }
            set
            {
                _binders = value;
            }
        }

        private IModelMetadataProvider ModelMetadataProvider
        {
            get
            {
                if(_metadataProvider == null)
                {
                    _metadataProvider = _services.GetRequiredService<IModelMetadataProvider>();
                }

                return _metadataProvider;
            }
        }

        private IServiceProvider _services;

        public OrdinaryModelBinder()
        {
        }


        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            if (controllerContext == null)
            {
                throw new ArgumentNullException(nameof(controllerContext));
            }

            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            if (bindingContext.ModelName != null &&
                !bindingContext.ValueProvider.ContainsPrefix(bindingContext.ModelName))
            {
                // 找不到指定前缀则直接返回Null
                return null;
            }

            if (bindingContext.ModelMetadata.IsComplexType)
            {
                return BindComplexModel(controllerContext, bindingContext);
            }
            else
            {
                var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
                if (valueProviderResult != null)
                    return BindSimpleModel(controllerContext, bindingContext, valueProviderResult);

                return null;
            }
        }


        private object BindComplexModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            if (bindingContext.Model != null &&
                bindingContext.ModelType.IsInstanceOfType(bindingContext.Model))
            {
                return bindingContext.Model;
            }


            if (TypeHelper.IsMatch(bindingContext.ModelType, typeof(IDictionary<,>)))
            {
                return BindDictionary(controllerContext, bindingContext);
            }

            if (TypeHelper.IsMatch(bindingContext.ModelType, typeof(IEnumerable<>)))
            {
                return BindCollection(controllerContext, bindingContext);
            }

            var model = bindingContext.Model;
            var modelType = bindingContext.ModelType;
            var valueProviderResult = bindingContext.ValueProvider;

            // 我们先创建一个空的Model对象，并将引用赋给元数据的Model
            model = CreateModel(controllerContext, bindingContext, modelType);
            bindingContext.ModelMetadata.Model = model;


            var propertyDescriptors = GetPropertyDescriptors(modelType);
            foreach (var propertyDescriptor in propertyDescriptors)
            {
                // 调用BindProperty方法为相应的属性赋值
                BindingProperty(controllerContext, bindingContext, propertyDescriptor);
            }

            return model;
        }

        private void BindingProperty(ControllerContext controllerContext, ModelBindingContext bindingContext, PropertyDescriptor property)
        {
            // 将属性名添加到现有前缀上
            string prefix = $"{bindingContext.ModelName ?? ""}.{property.Name ?? ""}".Trim('.');

            // 针对属性创建绑定上下文
            ModelMetadata metadata = bindingContext.PropertyMetadata[property.Name];
            ModelBindingContext context = new ModelBindingContext(bindingContext.ValueProvider)
            {
                ModelName = prefix,
                ModelMetadata = metadata,
            };

            // 针对属性实施Model绑定并对属性赋值
            // 注意BindModel方法的调用，复杂类型的递归调用就来自于这里
            object propertyValue = Binders.GetBinder(property.PropertyType).BindModel(controllerContext, context);

            if (bindingContext.ModelMetadata.ConvertEmptyStringToNull &&
                Object.Equals(propertyValue, String.Empty))
            {
                propertyValue = null;
            }

            context.ModelMetadata.Model = propertyValue;
            property.SetValue(bindingContext.Model, propertyValue);
        }

        private object BindCollection(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            Type modelType = bindingContext.ModelType;
            Type elementType;
            if (modelType.IsArray)
            {
                elementType = modelType.GetElementType();
            }
            else if ((elementType = TypeHelper.ExtractGenericInterface(modelType, typeof(IEnumerable<>))) != null)
            {
                elementType = elementType.GetGenericArguments()[0];
            }
            else
            {
                // modelType既不是集合类型也不是数组
                return null;
            }

            // 如果modelType是一个数组，则用List去填充它
            Type collectionType = modelType.IsArray ? typeof(List<>)
                .MakeGenericType(elementType) : modelType;

            // 调用CreateModel方法创建一个空的集合对象，之后将去填充它
            object model = CreateModel(controllerContext, bindingContext, collectionType);
            bindingContext.ModelMetadata.Model = model;

            // 针对每个索引实施Model绑定，并将绑定生成的元素添加到集合之中
            bool isZeroBased = GetIndexes(bindingContext, out var indexes);
            var elements = new List<object>();

            foreach (string index in indexes)
            {
                string prefix = $"{bindingContext.ModelName}[{index}]";
                if (!bindingContext.ValueProvider.ContainsPrefix(prefix))
                {
                    if (isZeroBased)
                    {
                        // 零基索引必须是连续的
                        break;
                    }

                    continue;
                }

                ModelBindingContext context = new ModelBindingContext(bindingContext.ValueProvider)
                {
                    ModelMetadata = ModelMetadataProvider.GetMetadataForType(null, elementType),
                    ModelName = prefix,
                };

                object element = Binders.GetBinder(elementType)
                    .BindModel(controllerContext, context);

                elements.Add(element);
            }

            ReplaceCollection(elementType, bindingContext.Model, elements);

            // 如果绑定的目标类型为数组，则创建一个空数组对象并向其赋值绑定生成的集合元素
            if (modelType.IsArray)
            {
                IList list = model as IList;
                if (list == null)
                {
                    return null;
                }

                Array array = Array.CreateInstance(elementType, list.Count);
                list.CopyTo(array, 0);
                return array;
            }

            return model;
        }

        private object BindDictionary(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            Type modelType = bindingContext.ModelType;
            Type[] argumentTypes = modelType.GetGenericArguments();
            Type keyType = argumentTypes[0];
            Type valueType = argumentTypes[1];
            object model = CreateModel(controllerContext, bindingContext, modelType);

            List<KeyValuePair<object, object>> list = new List<KeyValuePair<object, object>>();
            var isZeroBased = GetIndexes(bindingContext, out var indexes);

            foreach (var index in indexes)
            {
                string prefix = $"{bindingContext.ModelName}.[{index}]";
                if (!bindingContext.ValueProvider.ContainsPrefix(prefix))
                {
                    if (isZeroBased)
                    {
                        break;
                    }

                    continue;
                }

                ModelBindingContext contextForKey = new ModelBindingContext(bindingContext.ValueProvider)
                {
                    ModelMetadata = ModelMetadataProvider.GetMetadataForType(null, keyType),
                    ModelName = prefix + ".key"
                };

                ModelBindingContext contextForValue = new ModelBindingContext(bindingContext.ValueProvider)
                {
                    ModelMetadata = ModelMetadataProvider.GetMetadataForType(null, valueType),
                    ModelName = prefix + ".value"
                };

                object key = Binders.GetBinder(keyType)
                    .BindModel(controllerContext, contextForKey);
                object value = Binders.GetBinder(valueType)
                    .BindModel(controllerContext, contextForValue);
                list.Add(new KeyValuePair<object, object>(key, value));
            }

            ReplaceDictionary(keyType, valueType, model, list);
            return model;
        }

        /// <summary>
        /// 根据指定的ModelBindingContext得到当前所有索引
        /// </summary>
        /// <param name="bindingContext"></param>
        /// <param name="indexes"></param>
        /// <returns>返回的布尔值表示是否是零基索引</returns>
        private static bool GetIndexes(ModelBindingContext bindingContext, out IEnumerable<string> indexes)
        {
            string indexKey = CreateSubPropertyName(bindingContext.ModelName, "index");
            ValueProviderResult valueProviderResult = bindingContext.ValueProvider.GetValue(indexKey);

            if (valueProviderResult != null)
            {
                if (valueProviderResult.ConvertTo(typeof(string[])) is string[] indexesArray)
                {
                    indexes = indexesArray;
                    return false;
                }
            }

            indexes = GetZeroBasedIndexes();
            return true;
        }

        private static IEnumerable<string> GetZeroBasedIndexes()
        {
            int index = 0;
            while (true)
            {
                // 虽然这里是死循环，但我们规定了零基索引必须连续
                // 因此当调用方用所得到的索引无法找到指定的值时
                // 将会由调用方break
                yield return index.ToString(CultureInfo.InvariantCulture);
                index++;
            }
        }
        private static object BindSimpleModel(ControllerContext controllerContext, ModelBindingContext bindingContext, ValueProviderResult result)
        {
            if (bindingContext.ModelType.IsInstanceOfType(result.RawValue))
            {
                return result.RawValue;
            }

            // 由于string是char的数组形式，所以我们要筛选出这种情况
            if (bindingContext.ModelType != typeof(string) &&
                !bindingContext.ModelType.IsArray)
            {
                var enumerableType = TypeHelper.ExtractGenericInterface(bindingContext.ModelType, typeof(IEnumerable<>));

                if (enumerableType != null)
                {
                    // ModelType是一个集合而不是数组
                    // 我们先构造出数组再填充到所对应的集合中

                    object modelCollection = CreateModel(controllerContext, bindingContext, bindingContext.ModelType);

                    var elementType = enumerableType.GetGenericArguments()[0]; // 得到泛型参数类型
                    var arrayType = elementType.MakeArrayType(); // 构造出泛型参数类型的所对应的数组
                    object modelArray = ConvertProviderResult(result, arrayType); // 得到数组形式的值

                    Type collectionType = typeof(ICollection<>).MakeGenericType(elementType);  // ICollection<>是所有集合的公共接口
                    if (collectionType.IsInstanceOfType(modelCollection))
                    {
                        ReplaceCollection(elementType, modelCollection, modelArray); // 用数组去填充集合
                    }

                    return modelCollection;
                }
            }

            return ConvertProviderResult(result, bindingContext.ModelType);
        }


        private static object ConvertProviderResult(ValueProviderResult valueProviderResult, Type destinationType)
        {
            try
            {
                return valueProviderResult.ConvertTo(destinationType);
            }
            catch
            {
                return null;
            }
        }

        private static object CreateModel(ControllerContext controllerContext, ModelBindingContext bindingContext, Type modelType)
        {
            var targetType = modelType;

            if (modelType.IsGenericType)
            {
                // 获取开放泛型类型
                Type genericTypeDefinition = modelType.GetGenericTypeDefinition();

                if (genericTypeDefinition == typeof(IDictionary<,>))
                {
                    targetType = typeof(Dictionary<,>)
                        .MakeGenericType(modelType.GetGenericArguments());
                }
                else if (new Type[]
                {
                    typeof(ICollection<>),
                    typeof(IEnumerable<>),
                    typeof(IList<>)
                }
                .Any(x => x == genericTypeDefinition))
                {
                    targetType = typeof(List<>)
                        .MakeGenericType(modelType.GetGenericArguments());
                }
            }

            return Activator.CreateInstance(targetType);
        }

        private static void ReplaceCollection(Type collectionType, object collection, object source)
        {
            typeof(OrdinaryModelBinder)
                .GetMethod("CopyCollection", BindingFlags.Static | BindingFlags.NonPublic)
                .MakeGenericMethod(collectionType)
                .Invoke(null, new object[] { collection, source });
        }

        private static void CopyCollection<T>(ICollection<T> collection, IEnumerable source)
        {
            collection.Clear();
            if (source != null)
            {
                foreach (object item in source)
                {
                    collection.Add((item is T) ? (T)item : default);
                }
            }
        }

        private static void ReplaceDictionary(Type keyType, Type valueType, object destination, IEnumerable<KeyValuePair<object, object>> source)
        {
            typeof(OrdinaryModelBinder).GetMethod("CopyDictionary", BindingFlags.Static | BindingFlags.NonPublic)
                .MakeGenericMethod(keyType, valueType)
                .Invoke(null, new object[] { destination, source });
        }

        private static void CopyDictionary<TKey, TValue>(IDictionary<TKey, TValue> destination, IEnumerable<KeyValuePair<object, object>> source)
        {
            destination.Clear();
            foreach (var item in source)
            {
                if (item.Key is TKey)
                {
                    destination.Add((TKey)item.Key, item.Value is TValue ? (TValue)item.Value : default);
                }
            }
        }

        private static string CreateSubIndexName(string prefix, int index)
        {
            return String.Format(CultureInfo.InvariantCulture, "{0}[{1}]", prefix, index);
        }

        private static string CreateSubIndexName(string prefix, string index)
        {
            return String.Format(CultureInfo.InvariantCulture, "{0}[{1}]", prefix, index);
        }

        private static string CreateSubPropertyName(string prefix, string propertyName)
        {
            if (String.IsNullOrWhiteSpace(prefix))
            {
                return propertyName;
            }
            else if (String.IsNullOrWhiteSpace(propertyName))
            {
                return prefix;
            }
            else
            {
                return prefix + "." + propertyName;
            }
        }

        private static object GetPropertyValue(ControllerContext controllerContext, ModelBindingContext bindingContext, PropertyDescriptor propertyDescriptor, IModelBinder propertyBinder)
        {
            object value = propertyBinder.BindModel(controllerContext, bindingContext);

            if (bindingContext.ModelMetadata.ConvertEmptyStringToNull && Equals(value, String.Empty))
            {
                return null;
            }

            return value;
        }

        private static PropertyDescriptor[] GetPropertyDescriptors(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return type.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(x => x.GetGetMethod() != null && x.GetSetMethod() != null)
                .Select(x => new PropertyDescriptor
                {
                    Name = x.Name,
                    PropertyInfo = x,
                    PropertyType = x.PropertyType
                })
                .ToArray();
        }

        public class PropertyDescriptor
        {
            public string Name { get; set; }
            public PropertyInfo PropertyInfo { get; set; }
            public Type PropertyType { get; set; }

            public void SetValue(object model, object propertyValue)
            {
                PropertyInfo?.SetValue(model, propertyValue);
            }

            public object GetValue(object model)
            {
                return PropertyInfo?.GetValue(model);
            }
        }
    }
}