using KiraNet.GutsMvc.Implement;
using System.Collections.Generic;

namespace KiraNet.GutsMvc.Filter
{
    public interface IControllerFilterAttributeProvider
    {
        IEnumerable<FilterAttribute> GetControllerFilterAttributes(ControllerDescriptor controllerDescriptor);
    }
}
