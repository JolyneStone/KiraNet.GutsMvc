using KiraNet.GutsMvc.Filter;
using KiraNet.GutsMvc.Implement;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading;


namespace KiraNet.GutsMvc
{
    public abstract class ActionInvoker : IActionInvoker
    {
        void IActionInvoker.InvokeAction(ControllerContext controllerContext)
        {
            ImplementInvokeAction(controllerContext, InvokeAction);
        }

        void IActionInvoker.InvokeActionAsync(ControllerContext controllerContext)
        {
            ImplementInvokeAction(controllerContext, InvokeActionAsync);
        }

        protected virtual void InvokeAction(ControllerContext controllerContext, object[] paramValues)
        {
        }

        protected virtual void InvokeActionAsync(ControllerContext controllerContext, object[] paramValues)
        {
        }

        private void ImplementInvokeAction(ControllerContext controllerContext, Action<ControllerContext, Object[]> invokeAction)
        {
            var filterCollection = GetFilters(controllerContext);

            try
            {
                AuthenticationContext authenticationContext = AuthenticationInvoke(controllerContext, filterCollection.AuthenticationFilters);
                if (authenticationContext.Result != null)
                {
                    InvokeActionResult(controllerContext, authenticationContext.Result);
                    return;
                }

                AuthorizationContext authorizationContext = AuthorizationInvoke(controllerContext, filterCollection.AuthorizationFilters);
                if (authorizationContext.Result != null)
                {
                    InvokeActionResult(controllerContext, authorizationContext.Result);
                    return;
                }

                var paramValues = GetParameterValues(controllerContext.ParameterDescriptors);

                //ResultExecutingContext resultExecutingContext = ResultExecutingInvoke(controllerContext, filterCollection.ResultFilter);
                //if (resultExecutingContext.Result != null)
                //{
                //    InvokeActionResult(controllerContext, resultExecutingContext.Result);
                //}

                ActionExecutingContext actionExecutingContext = ActionExecutingInvoke(controllerContext, filterCollection.ActionFilter, paramValues);
                if (actionExecutingContext.Result != null)
                {
                    InvokeActionResult(controllerContext, actionExecutingContext.Result);
                }

                Exception exception = null;
                try
                {
                    invokeAction(controllerContext, paramValues);
                }
                catch (Exception ex)
                {
                    exception = ex;
                }
                finally
                {
                    ActionExecutedContext actionExecutedContext = ActionExecutedInvoke(controllerContext, filterCollection.ActionFilter, exception);
                    if (actionExecutedContext.Result != null)
                    {
                        InvokeActionResult(controllerContext, actionExecutedContext.Result);
                    }

                    ResultExecutedContext resultExecutedContext = ResultExecutedInvoke(controllerContext, filterCollection.ResultFilter, exception);
                    if (resultExecutedContext.Result != null)
                    {
                        InvokeActionResult(controllerContext, resultExecutedContext.Result);
                    }

                    if (exception != null)
                    {
                        throw exception;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionContext exceptionContext = ExceptionInvoke(controllerContext, filterCollection.ExceptionFilter, ex);
                if (exceptionContext.Result == null)
                {
                    throw;
                }

                InvokeActionResult(controllerContext, exceptionContext.Result);
            }
        }


        private static AuthenticationContext AuthenticationInvoke(ControllerContext controllerContext, IList<IAuthenticationFilter> filters)
        {
            IPrincipal principal = controllerContext.HttpContext.User;
            var context = new AuthenticationContext(controllerContext, principal);

            foreach (var filter in filters)
            {
                filter.OnAuthentication(context);
                if (context.Result != null)
                {
                    break;
                }
            }

            if (context.Principal != principal)
            {
                controllerContext.HttpContext.User = context.Principal;
                Thread.CurrentPrincipal = context.Principal;
            }

            return context;
        }

        private static AuthorizationContext AuthorizationInvoke(ControllerContext controllerContext, IList<IAuthorizationFilter> filters)
        {
            var context = new AuthorizationContext(controllerContext);
            foreach (var filter in filters)
            {
                filter.OnAuthorization(context);
                if (context.Result != null)
                {
                    return context;
                }
            }

            return context;
        }

        private static ActionExecutingContext ActionExecutingInvoke(ControllerContext controllerContext, IList<IActionFilter> filters, object[] paramValues)
        {
            var context = new ActionExecutingContext(controllerContext, paramValues);
            foreach (var filter in filters)
            {
                filter.OnActionExecuting(context);
                if (context.Result != null)
                {
                    return context;
                }
            }

            return context;
        }

        private static ActionExecutedContext ActionExecutedInvoke(ControllerContext controllerContext, IList<IActionFilter> filters, Exception ex)
        {
            var context = new ActionExecutedContext(controllerContext, ex);
            foreach (var filter in filters)
            {
                filter.OnActionExecuted(context);
                if (context.Result != null)
                {
                    return context;
                }
            }

            return context;
        }

        //private static ResultExecutingContext ResultExecutingInvoke(ControllerContext controllerContext, IList<IResultFilter> filters)
        //{
        //    var context = new ResultExecutingContext(controllerContext);
        //    foreach (var filter in filters)
        //    {
        //        filter.OnResultExecuting(context);
        //        if (context.Result != null)
        //        {
        //            return context;
        //        }
        //    }

        //    return context;
        //}

        private static ResultExecutedContext ResultExecutedInvoke(ControllerContext controllerContext, IList<IResultFilter> filters, Exception ex)
        {
            var context = new ResultExecutedContext(controllerContext, ex);
            foreach (var filter in filters)
            {
                filter.OnResultExecuted(context);
                if (context.Cancel && context.Result != null)
                {
                    return context;
                }
            }

            return context;
        }

        private static ExceptionContext ExceptionInvoke(ControllerContext controllerContext, IList<IExceptionFilter> filters, Exception ex)
        {
            var context = new ExceptionContext(controllerContext)
            {
                Exception = ex
            };

            foreach (var filter in filters)
            {
                filter.OnException(context);
                if (context.Result != null)
                {
                    return context;
                }
            }

            return context;
        }

        private static void InvokeActionResult(ControllerContext controllerContext, IActionResult actionResult)
        {
            if (actionResult == null)
            {
                throw new ArgumentNullException(nameof(actionResult));
            }

            actionResult.ExecuteResult(controllerContext);
        }

        private static FilterCollection GetFilters(ControllerContext controllerContext)
        {
            var filterProvider = controllerContext.HttpContext.Service.GetService<IFilterProvider>();

            var filters = new FilterInfoCollection(filterProvider.GetFilters(controllerContext));
            return new FilterCollection
            {
                AuthenticationFilters = GetFilters(filters.AuthenticationFilters),
                AuthorizationFilters = GetFilters(filters.AuthorizationFilters),
                ActionFilter = GetFilters(filters.ActionFilters),
                ResultFilter = GetFilters(filters.ResultFilters),
                ExceptionFilter = GetFilters(filters.ExceptionFilters)
            };
        }

        private static List<T> GetFilters<T>(List<FilterInfo<T>> list)
            where T : class
        {
            if (list == null)
            {
                return new List<T>();
            }

            return list
                .OrderBy(x => x.Order)
                .OrderBy(x => x.Scope)
                .Select(x => x.Filter)
                .ToList();
        }

        private static object[] GetParameterValues(ParameterDescriptor[] parameterDescriptors)
        {
            if (parameterDescriptors == null || parameterDescriptors.Length == 0)
            {
                return null;
            }

            object[] parameterValues = new object[parameterDescriptors.Length];
            for (int i = 0; i < parameterValues.Length; i++)
            {
                parameterValues[i] = parameterDescriptors[i].ParameterValue;
            }

            return parameterValues;
        }


        private class FilterCollection
        {
            public List<IAuthenticationFilter> AuthenticationFilters { get; set; }
            public List<IAuthorizationFilter> AuthorizationFilters { get; set; }
            public List<IActionFilter> ActionFilter { get; set; }
            public List<IResultFilter> ResultFilter { get; set; }
            public List<IExceptionFilter> ExceptionFilter { get; set; }
        }
    }
}
