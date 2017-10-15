using KiraNet.GutsMvc.Implement;
using System.Collections.Generic;

namespace KiraNet.GutsMvc.Filter
{
    /// <summary>
    /// 用于提供Controller这个特殊过滤器
    /// </summary>
    public class ControllerFilterProvider : IFilterProvider
    {
        public IEnumerable<Filter> GetFilters(ControllerContext controllerContext)
        {
            return new Filter[] { new Filter(controllerContext.Controller, FilterScope.First, int.MinValue) };
        }
    }
}
