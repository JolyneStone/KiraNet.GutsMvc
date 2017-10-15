using System.Collections.Generic;

namespace KiraNet.GutsMvc.Filter
{
    public interface IFilterAttributeProvider
    {
        IEnumerable<FilterAttribute> GetFilterAttributes();
    }
}
