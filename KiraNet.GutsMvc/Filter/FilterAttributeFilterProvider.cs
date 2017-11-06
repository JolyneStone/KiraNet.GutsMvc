using KiraNet.GutsMvc.Helper;
using KiraNet.GutsMvc.Implement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace KiraNet.GutsMvc.Filter
{
    /// <summary>
    /// 用于提供应用FilterAttribute特性注册的过滤器
    /// </summary>
    public class FilterAttributeFilterProvider : IFilterProvider, IControllerFilterAttributeProvider, IActionFilterAttributeProvider
    {
        private KiraSpinLock _sync = new KiraSpinLock();
        private Dictionary<Type, List<FilterAttribute>> _controllerFilters = new Dictionary<Type, List<FilterAttribute>>();
        private Dictionary<MethodInfo, List<FilterAttribute>> _actionFilters = new Dictionary<MethodInfo, List<FilterAttribute>>();

        public IEnumerable<Filter> GetFilters(ControllerContext controllerContext)
        {
            var filters = new List<Filter>();

            var controllerFilterAttributes = GetControllerFilterAttributes(controllerContext.ControllerDescriptor);
            var actionFilterAttributes = GetActionFilterAttributes(controllerContext.ActionDescriptor);
            if(controllerFilterAttributes != null && controllerFilterAttributes.Any())
            {
                filters.AddRange(controllerFilterAttributes
                    .Select(attribute => CreateFilter(attribute, FilterScope.Controller)));
            }

            if(actionFilterAttributes != null && actionFilterAttributes.Any())
            {
                filters.AddRange(actionFilterAttributes
                    .Select(attribute => CreateFilter(attribute, FilterScope.Action)));
            }

            return filters;
        }

        private static Filter CreateFilter(FilterAttribute filterAttribute, FilterScope scope)
        {
            if (filterAttribute == null)
            {
                throw new ArgumentNullException(nameof(filterAttribute));
            }

            return new Filter(filterAttribute, scope, filterAttribute.Order);
        }

        public IEnumerable<FilterAttribute> GetControllerFilterAttributes(ControllerDescriptor controllerDescriptor)
        {
            if (controllerDescriptor == null)
            {
                throw new ArgumentNullException(nameof(controllerDescriptor));
            }

            if (controllerDescriptor.ControllerType == null)
            {
                throw new ArgumentNullException(nameof(controllerDescriptor.ControllerType));
            }

            var controllerType = controllerDescriptor.ControllerType;

            _sync.Enter();
            _controllerFilters.TryGetValue(controllerType, out var filterAttributes);
            _sync.Exit();

            if (filterAttributes != null)
            {
                return filterAttributes;
            }

            filterAttributes = controllerDescriptor.GetFilterAttributes()?.ToList();

            if (filterAttributes == null && filterAttributes.Count > 0)
            {
                return null;
            }

            _sync.Enter();
            _controllerFilters[controllerType] = filterAttributes;
            _sync.Exit();

            return filterAttributes;
        }

        public IEnumerable<FilterAttribute> GetActionFilterAttributes(ActionDescriptor actionDescriptor)
        {
            if (actionDescriptor == null)
            {
                throw new ArgumentNullException(nameof(actionDescriptor));
            }

            if (actionDescriptor.Action == null)
            {
                throw new ArgumentNullException(nameof(actionDescriptor.Action));
            }

            var action = actionDescriptor.Action;

            _sync.Enter();
            _actionFilters.TryGetValue(action, out var filterAttributes);
            _sync.Exit();

            if (filterAttributes != null)
            {
                return filterAttributes;
            }

            filterAttributes = actionDescriptor.GetFilterAttributes()?.ToList();

            if (filterAttributes == null && filterAttributes.Count > 0)
            {
                return null;
            }

            _sync.Enter();
            _actionFilters[action] = filterAttributes;
            _sync.Exit();

            return filterAttributes;
        }
    }
}
