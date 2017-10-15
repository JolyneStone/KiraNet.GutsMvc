using KiraNet.GutsMvc.Implement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace KiraNet.GutsMvc.Filter
{
    public class GlobalFilterCollection : IEnumerable<Filter>, IFilterProvider
    {
        private List<Filter> _filters = new List<Filter>();

        public static GlobalFilterCollection GlobalFilter { get; } = new GlobalFilterCollection();

        public int Count => _filters.Count;

        public IEnumerable<Filter> GetFilters(ControllerContext controllerContext) => this;

        public bool Contains(object filter)
        {
            if (filter == null)
            {
                return false;
            }

            return _filters.Any(x => x.Instance == filter);
        }

        public void Clear()
        {
            _filters.Clear();
        }

        public void Add(IAuthenticationFilter filter, int? order = null)
        {
            Add(filter, order);
        }

        public void Add(IAuthorizationFilter filter, int? order = null)
        {
            Add(filter, order);
        }

        public void Add(IActionFilter filter, int? order = null)
        {
            Add(filter, order);
        }

        public void Add(IResultFilter filter, int? order = null)
        {
            Add(filter, order);
        }

        public void Add(IExceptionFilter filter, int? order = null)
        {
            Add(filter, order);
        }

        private void Add(object filter, int? order)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            _filters.Add(new Filter(filter, FilterScope.Global, order));
        }

        public IEnumerator<Filter> GetEnumerator()
        {
            return ((IEnumerable<Filter>)_filters).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<Filter>)_filters).GetEnumerator();
        }
    }
}
