using System;

namespace KiraNet.GutsMVC
{
    [AttributeUsage(
        AttributeTargets.Method,
        AllowMultiple = false,
        Inherited = true)]
    public class NonActionAttribute: Attribute
    {
    }
}
