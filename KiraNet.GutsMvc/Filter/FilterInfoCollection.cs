using System;
using System.Collections.Generic;

namespace KiraNet.GutsMvc.Filter
{
    public class FilterInfoCollection
    {
        public FilterInfoCollection(IEnumerable<Filter> filters)
        {
            if (filters == null)
            {
                throw new ArgumentNullException(nameof(filters));
            }

            foreach(var filter in filters)
            {
                AddFilter(filter);
            }
        }

        public List<FilterInfo<IAuthenticationFilter>> AuthenticationFilters { get; } = new List<FilterInfo<IAuthenticationFilter>>();
        public List<FilterInfo<IAuthorizationFilter>> AuthorizationFilters { get; } = new List<FilterInfo<IAuthorizationFilter>>();
        public List<FilterInfo<IActionFilter>> ActionFilters { get; } = new List<FilterInfo<IActionFilter>>();
        public List<FilterInfo<IResultFilter>> ResultFilters { get; } = new List<FilterInfo<IResultFilter>>();
        public List<FilterInfo<IExceptionFilter>> ExceptionFilters { get; } = new List<FilterInfo<IExceptionFilter>>();

        private void AddFilter(Filter filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            var instance = filter.Instance;
            if(instance is IAuthenticationFilter authentication)
            {
                AuthenticationFilters.Add(new FilterInfo<IAuthenticationFilter>
                {
                    Filter = authentication,
                    Scope = filter.Scope,
                    Order = filter.Order
                });
            }

            if(instance is IAuthorizationFilter authorization)
            {
                AuthorizationFilters.Add(new FilterInfo<IAuthorizationFilter>
                {
                    Filter = authorization,
                    Scope = filter.Scope,
                    Order = filter.Order
                });
            }

            if(instance is IActionFilter action)
            {
                ActionFilters.Add(new FilterInfo<IActionFilter>
                {
                    Filter = action,
                    Scope = filter.Scope,
                    Order = filter.Order
                });
            }

            if(instance is IResultFilter result)
            {
                ResultFilters.Add(new FilterInfo<IResultFilter>
                {
                    Filter = result,
                    Scope = filter.Scope,
                    Order = filter.Order
                });
            }

            if(instance is IExceptionFilter exception)
            {
                ExceptionFilters.Add(new FilterInfo<IExceptionFilter>
                {
                    Filter = exception,
                    Scope = filter.Scope,
                    Order = filter.Order
                });
            }
        }
    }
}
