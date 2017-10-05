using System;

namespace KiraNet.GutsMVC
{
    /// <summary>
    /// 用于标记非Controller类
    /// </summary>
    [AttributeUsage(AttributeTargets.Class,
        AllowMultiple = false,
        Inherited = true)]
    public class NonControllerAttribute:Attribute
    {
    }
}
