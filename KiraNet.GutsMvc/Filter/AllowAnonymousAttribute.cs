using System;

namespace KiraNet.GutsMvc.Filter
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
