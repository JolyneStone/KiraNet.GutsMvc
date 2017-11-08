using KiraNet.GutsMvc.Helper;
using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;

namespace KiraNet.GutsMvc.Infrastructure
{
    [Serializable]
    public class ValueProviderResult
    {
        private static readonly CultureInfo _culture = CultureInfo.InvariantCulture;
        private CultureInfo _instanceCulture;

        protected ValueProviderResult()
        {
        }

        public ValueProviderResult(object rawValue, string strRawValue, CultureInfo culture)
        {
            RawValue = rawValue;
            StrRawValue = strRawValue;
            Culture = culture;
        }

        public CultureInfo Culture
        {
            get
            {
                if (_instanceCulture == null)
                {
                    _instanceCulture = _culture;
                }
                return _instanceCulture;
            }
            protected set { _instanceCulture = value; }
        }

        /// <summary>
        /// 表示被封装的原始数据
        /// </summary>
        public object RawValue { get; protected set; }

        /// <summary>
        /// 原始数据的字符串表示
        /// </summary>
        public string StrRawValue { get; protected set; }


        /// <summary>
        /// 对于大部分的ValueProvider对象来说，原始数据都以字符串的形式存放在被封装的容器中。
        /// 在进行Model绑定的时候需要根据目标参数的元数据信息将其转换成对应的类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public object ConvertTo(Type type)
        {
            return ConvertTo(type, null);
        }

        public virtual object ConvertTo(Type type, CultureInfo culture)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            CultureInfo cultureToUse = culture ?? Culture;
            return PossibleArrayType(cultureToUse, StrRawValue, type);
        }

        private static object PossibleArrayType(CultureInfo culture, object value, Type destinationType)
        {
            if (value == null || destinationType.IsInstanceOfType(value)) // value是否是destinationType的实例
            {
                return value;
            }

            // 数组的转换有以下四种情况
            Array valueAsArray = value as Array;
            if (destinationType.IsArray)
            {
                Type destinationElementType = destinationType.GetElementType(); // 得到数组的元素类型
                if (valueAsArray != null)
                {
                    // case 1: 目标类型和value都是数组类型，则依次转换各个元素的类型
                    IList converted = Array.CreateInstance(destinationElementType, valueAsArray.Length); // 创建数组
                    for (int i = 0; i < valueAsArray.Length; i++)
                    {
                        converted[i] = ConvertSimpleType(culture, valueAsArray.GetValue(i), destinationElementType);
                    }
                    return converted;
                }
                else
                {
                    // case 2: value不是数组而目标类型是数组，则value可能是目标类型所表示的数组的一个元素
                    object element = ConvertSimpleType(culture, value, destinationElementType);
                    IList converted = Array.CreateInstance(destinationElementType, 1);
                    converted[0] = element;
                    return converted;
                }
            }
            else if (valueAsArray != null)
            {
                // case 3: 目标类型是value的一个元素，则提取value的第一个元素
                if (valueAsArray.Length > 0)
                {
                    value = valueAsArray.GetValue(0);
                    return ConvertSimpleType(culture, value, destinationType);
                }
                else
                {
                    return null;
                }
            }
            // case 4: value和目标类型都是单一类型（不是集合或数组），则尝试直接转换
            return ConvertSimpleType(culture, value, destinationType);
        }


        private static object ConvertSimpleType(CultureInfo culture, object value, Type destinationType)
        {
            if (value == null || destinationType.IsInstanceOfType(value))
            {
                return value;
            }

            string valueAsString = value as string;
            if (valueAsString != null && 
                destinationType == typeof(string) &&
                String.IsNullOrWhiteSpace(valueAsString))
            {
                return String.Empty;
            } // 进行到下面的代码则表示value非字符串类型或为非空字符串

            // 如果该类型是可空类型，则尝试提取出基础类型
            Type underlyingType = Nullable.GetUnderlyingType(destinationType);
            if (underlyingType == null)
            {
                //destinationType = underlyingType;
                underlyingType = destinationType;
            }

            if (valueAsString == null)
            {
                // 如果value非字符串类型，则尝试转换成IConvertible，并转换成指定类型的值
                if (value is IConvertible convertible)
                {
                    try
                    {
                        return convertible.ToType(underlyingType, culture);
                    }
                    catch
                    {
                        return null;
                    }
                }
            }

            // 将目标类型作为转换器
            TypeConverter converter = TypeDescriptor.GetConverter(underlyingType);
            bool canConvertFrom = converter.CanConvertFrom(value.GetType()); // 是否能够将value类型转换为目标类型
            if (!canConvertFrom)
            {
                // 如果不能将value转换为目标类型，则用value作为转换器
                converter = TypeDescriptor.GetConverter(value.GetType());
            }

            if (!(canConvertFrom || converter.CanConvertTo(underlyingType))) 
            {
                // 运行这里可以确定不能将value转换为目标类型，则用value作为转换器
                // 尝试将int转换成enum
                if (underlyingType.IsEnum && value is int)
                {
                    return Enum.ToObject(underlyingType, (int)value);
                }
                
                throw new InvalidOperationException(String.Format(
                    CultureInfo.CurrentCulture, "无法将{0}转换为{1}", value.GetType().FullName, underlyingType.FullName));
            }

            try
            {
                object convertedValue = (canConvertFrom)
                                            ? converter.ConvertFrom(null, culture, value)
                                            : converter.ConvertTo(null, culture, value, underlyingType);
                return convertedValue;
            }
            catch (Exception ex)
            {
                if (TypeHelper.TypeAllowsNullValue(destinationType))
                {
                    return null;
                }
                else
                {
                    throw new InvalidOperationException(String.Format(
                        CultureInfo.CurrentCulture, "无法将{0}转换为{1}", value.GetType().FullName, destinationType.FullName),
                        ex);
                }
            }
        }
    }
}