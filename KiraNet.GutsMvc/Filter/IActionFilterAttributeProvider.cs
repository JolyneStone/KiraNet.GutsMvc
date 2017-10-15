using KiraNet.GutsMvc.Implement;
using System.Collections.Generic;

namespace KiraNet.GutsMvc.Filter
{
    public interface IActionFilterAttributeProvider
    {
        IEnumerable<FilterAttribute> GetActionFilterAttributes(ActionDescriptor actionDescriptor);
    }
}
