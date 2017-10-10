using System.Collections.Generic;
using System.Reflection;

namespace KiraNet.GutsMvc.Infrastructure
{
    public interface IAssemblyLocator
    {
        IEnumerable<Assembly> DependencyAssemblies();
        IEnumerable<Assembly> ReferenceAssemblies();
    }
}
