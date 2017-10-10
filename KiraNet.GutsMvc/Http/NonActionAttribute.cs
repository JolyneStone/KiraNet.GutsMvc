using System;

namespace KiraNet.GutsMvc
{
    [AttributeUsage(
        AttributeTargets.Method,
        AllowMultiple = false,
        Inherited = true)]
    public class NonActionAttribute: Attribute
    {
    }
}
