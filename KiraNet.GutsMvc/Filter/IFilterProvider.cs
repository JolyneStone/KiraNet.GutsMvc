using KiraNet.GutsMvc.Implement;
using System.Collections.Generic;

namespace KiraNet.GutsMvc.Filter
{
    public interface IFilterProvider
    {
        IEnumerable<Filter> GetFilters(ControllerContext controllerContext);
    }
}
