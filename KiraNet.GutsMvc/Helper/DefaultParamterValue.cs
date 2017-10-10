using System;
using System.Reflection;

namespace KiraNet.GutsMvc.Helper
{
    public class DefaultParamterValue
    {
        /// <summary>
        /// 注：如果该参数类型为DateTime，则默认值为default(DateTime)
        /// </summary>
        /// <param name="parameter"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static bool TryGetDefaultValue(ParameterInfo parameter, out object defaultValue)
        {
            bool hasDefaultValue;
            var tryToGetDefaultValue = true;
            defaultValue = null;

            try
            {
                hasDefaultValue = parameter.HasDefaultValue;
            }
            catch (FormatException) when (parameter.ParameterType == typeof(DateTime)) // 如果该参数类型为DateTime，则会抛出此异常 因为DateTime的任何值都不是编译时常量
                                                                                       // 详见 https://github.com/dotnet/corefx/issues/12338
            {
                hasDefaultValue = true;
                tryToGetDefaultValue = false;
            }

            if (hasDefaultValue)
            {
                if (tryToGetDefaultValue)
                {
                    defaultValue = parameter.DefaultValue;
                }

                if (defaultValue == null && parameter.ParameterType.GetTypeInfo().IsValueType)
                {
                    defaultValue = Activator.CreateInstance(parameter.ParameterType);
                }
            }

            return hasDefaultValue;
        }
    }
}
