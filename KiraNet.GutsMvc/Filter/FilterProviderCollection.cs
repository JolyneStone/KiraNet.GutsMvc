using KiraNet.GutsMvc.Implement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace KiraNet.GutsMvc.Filter
{
    public class FilterProviderCollection:ICollection<IFilterProvider>, IFilterProvider
    {
        private List<IFilterProvider> _filterProvider = new List<IFilterProvider>();

        public IEnumerable<Filter> GetFilters(ControllerContext controllerContext)
        {
            if (controllerContext == null)
            {
                throw new ArgumentNullException(nameof(controllerContext));
            }

            var filters = new List<Filter>();
            foreach(var filterProvider in _filterProvider)
            {
                var tempFilters = filterProvider.GetFilters(controllerContext);

                if(tempFilters!=null && tempFilters.Any())
                {
                    filters.AddRange(tempFilters);
                }
            }

            return filters;
        }

        public int Count => ((ICollection<IFilterProvider>)_filterProvider).Count;

        public bool IsReadOnly => ((ICollection<IFilterProvider>)_filterProvider).IsReadOnly;

        public void Add(IFilterProvider item)
        {
            ((ICollection<IFilterProvider>)_filterProvider).Add(item);
        }

        public void Clear()
        {
            ((ICollection<IFilterProvider>)_filterProvider).Clear();
        }

        public bool Contains(IFilterProvider item)
        {
            return ((ICollection<IFilterProvider>)_filterProvider).Contains(item);
        }

        public void CopyTo(IFilterProvider[] array, int arrayIndex)
        {
            ((ICollection<IFilterProvider>)_filterProvider).CopyTo(array, arrayIndex);
        }

        public IEnumerator<IFilterProvider> GetEnumerator()
        {
            return ((ICollection<IFilterProvider>)_filterProvider).GetEnumerator();
        }

        public bool Remove(IFilterProvider item)
        {
            return ((ICollection<IFilterProvider>)_filterProvider).Remove(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((ICollection<IFilterProvider>)_filterProvider).GetEnumerator();
        }
    }
}
