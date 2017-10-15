using System;

namespace KiraNet.GutsMvc.Filter
{
    [AttributeUsage(
        AttributeTargets.Method |
        AttributeTargets.Class,
        Inherited = true,
        AllowMultiple = false)]
    public abstract class FilterAttribute : Attribute, IFilter
    {
        public virtual bool AllowMultiple => false;

        public int Order { get; }
    }
}
