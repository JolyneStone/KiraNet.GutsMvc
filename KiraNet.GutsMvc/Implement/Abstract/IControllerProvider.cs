using System.Collections.Generic;
using System.Reflection;

namespace KiraNet.GutsMvc.Implement
{
    public interface IControllerProvider
    {
        void Populate(IEnumerable<TypeInfo> collection, ControllerCollection feature);
        ControllerCollection GetControllerCollection();
    }
}