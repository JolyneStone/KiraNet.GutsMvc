using System;

namespace KiraNet.GutsMvc
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
