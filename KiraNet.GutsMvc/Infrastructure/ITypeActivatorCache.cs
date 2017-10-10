using System;

namespace KiraNet.GutsMvc
{
    public interface ITypeActivatorCache
    {
        TInstance CreateInstance<TInstance>(IServiceProvider serviceProvider, Type implementationType);
    }
}
