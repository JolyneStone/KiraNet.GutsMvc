using System;

namespace KiraNet.GutsMVC
{
    [AttributeUsage(
        AttributeTargets.Class|
        AttributeTargets.Property,
        AllowMultiple = false,
        Inherited = true)]
    public class HiddenInputAttribute:Attribute
    {
        public bool DisplayValue { get; set; }

        public HiddenInputAttribute()
        {
            DisplayValue = true;
        }

        public HiddenInputAttribute(bool displayValue)
        {
            DisplayValue = displayValue;
        }
    }
}
