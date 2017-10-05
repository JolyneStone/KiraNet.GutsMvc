using System.Reflection;

namespace KiraNet.GutsMVC.Helper
{
    public interface IAssemblyLocator
    {
        Assembly[] GetAssemblies();
    }
}
