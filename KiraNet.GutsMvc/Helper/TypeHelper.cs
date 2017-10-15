using System;
using System.Linq;

namespace KiraNet.GutsMvc.Helper
{
    internal static class TypeHelper
    {
        public static bool TypeAllowsNullValue(Type type)
        {
            return (!type.IsValueType || Nullable.GetUnderlyingType(type) != null);
        }

        /// <summary>
        /// 判断指定的类型是否是可空类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsNullableValueType(Type type)
        {
            return Nullable.GetUnderlyingType(type) != null;
        }

        /// <summary>
        /// 判断指定的类型是否是基元类型
        /// 注：string不是基元类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsPrimitiveType(Type type)
        {
            return type.IsPrimitive;
        }

        /// <summary>
        /// 得到所实现的泛型接口
        /// </summary>
        /// <param name="queryType"></param>
        /// <param name="interfaceType"></param>
        /// <returns></returns>
        public static Type ExtractGenericInterface(Type queryType, Type interfaceType)
        {
            if(queryType.IsGenericType &&
                queryType.GetGenericTypeDefinition() == interfaceType)
            {
                return queryType;
            }

            var queryTypeInterfaces = queryType.GetInterfaces();

            return queryTypeInterfaces.FirstOrDefault(x => x.IsGenericType && x.GetGenericTypeDefinition() == interfaceType);
        }

        /// <summary>
        /// 判断某类型与其目标类型是否相匹配
        /// </summary>
        /// <param name="type"></param>
        /// <param name="targetType"></param>
        /// <returns></returns>
        public static bool IsMatch(Type type, Type targetType)
        {
            // case 1： targetType是开放类型
            if (type.IsGenericType &&
                type.GetGenericTypeDefinition() == targetType)
            {
                return true;
            }

            // case 2：type是否是targetType的实现类型
            if (targetType.IsAssignableFrom(type))
            {
                return true;
            }

            // case 3：泛型接口的匹配
            foreach (Type interfaceType in type.GetInterfaces())
            {
                if (interfaceType.IsGenericType &&
                    interfaceType.GetGenericTypeDefinition() == targetType)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
