using System;

namespace KiraNet.GutsMvc
{
    [AttributeUsage(
        AttributeTargets.Class |
        AttributeTargets.Method, 
        AllowMultiple = false,
        Inherited = true)]
    public sealed class AllowAnonymousAttribute : Attribute
    {
    }
}
