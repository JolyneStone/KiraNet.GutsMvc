using KiraNet.GutsMvc.Implement;
using KiraNet.GutsMvc.Infrastructure;
using KiraNet.GutsMvc.Route;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace KiraNet.GutsMvc
{
    /// <summary>
    /// Controller的抽象类
    /// </summary>
    public abstract class Controller : IDisposable
    {
        private IValueProvider _valueProvider;
        private ControllerContext _controllerContext;
        private ViewDataDictionary _viewDataDictionary;
        private TempDataDictionary _tempDataDictionary;
        private DynamicViewBag _viewBag;
        private IActionInvoker _actionInvoker;

        public HttpContext HttpContext => ControllerContext?.HttpContext;

        public HttpRequest HttpRequest => HttpContext?.Request;

        public HttpResponse HttpResponse => HttpContext?.Response;

        public RouteEntity RouteEntity { get; internal set; }

        public IActionInvoker ActionInvoker
        {
            get
            {
                if (_actionInvoker == null)
                {
                    _actionInvoker = new ActionInvokeProvider().GetActionInvoker();
                }

                return _actionInvoker;
            }
            set
            {
                _actionInvoker = value;
            }
        }

        public ControllerContext ControllerContext
        {
            get
            {
                if (_controllerContext == null)
                    _controllerContext = new ControllerContext()
                    {
                        Controller = this
                    };

                return _controllerContext;
            }
            set
            {
                _controllerContext = value ?? throw new ArgumentNullException(nameof(value));
            }
        }

        public ViewDataDictionary ViewData
        {
            get
            {
                if (_viewDataDictionary == null)
                {
                    _viewDataDictionary = new ViewDataDictionary();
                }

                return _viewDataDictionary;
            }
        }

        public TempDataDictionary TempData
        {
            get
            {
                if (_tempDataDictionary == null)
                {
                    _tempDataDictionary = new TempDataDictionary();
                }

                return _tempDataDictionary;
            }
        }

        public dynamic ViewBag
        {
            get
            {
                if (_viewBag == null)
                {
                    _viewBag = new DynamicViewBag(ViewData);
                }

                return _viewBag;
            }
        }

        public IValueProvider ValueProvider
        {
            get
            {
                if (_valueProvider == null)
                {
                    _valueProvider = ValueProviderFactories.Factories.GetValueProvider(ControllerContext);
                }

                return _valueProvider;
            }

            set
            {
                _valueProvider = value;
            }
        }

        /// <summary>
        /// 获取将执行的Action方法，并为之绑定参数
        /// 利用ActionInvoke属性执行Action方法
        /// </summary>
        internal virtual void Execute()
        {
            if (_valueProvider == null)
            {
                _valueProvider = ValueProviderFactories.Factories.GetValueProvider(ControllerContext);
            }

            ControllerContext.ActionDescriptor = ControllerContext.ControllerDescriptor.BindingAction(this);
            if (ControllerContext.ActionDescriptor == null)
            {
                throw new MissingMethodException($"无法找到指定的Action");
            }

            var returnType = ControllerContext.ActionDescriptor.Action.ReturnType;
            if (returnType.IsGenericType &&
                returnType.GetGenericTypeDefinition() == typeof(Task<>))
            {
                // 调用异步Action
                ActionInvoker.InvokeActionAsync(ControllerContext);
            }
            else
            {
                // 调用同步Action
                ActionInvoker.InvokeAction(ControllerContext);
            }
        }

        /// <summary>
        /// 具体函数体由实际的Controller实现
        /// </summary>
        public virtual void Dispose()
        {
        }

        /// <summary>
        /// 忽略对请求的响应
        /// </summary>
        /// <returns></returns>
        protected virtual EmptyResult Empty()
        {
            return new EmptyResult();
        }

        protected virtual ContentResult Content(string content, string contentType = null, Encoding encoding = null)
        {
            return new ContentResult
            {
                Content = content,
                ContentType = contentType,
                ContentEncoding = encoding
            };
        }

        protected virtual FileResult File(Stream fileStream, string fileName = null, string contentType = null)
        {
            if (fileStream == null)
            {
                throw new ArgumentNullException(nameof(fileStream));
            }

            return new FileResult
            {
                FileName = fileName,
                ContentType = contentType,
                FileStream = fileStream
            };
        }

        protected virtual FileResult File(string fileName, string contentType)
        {
            if (String.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            if (String.IsNullOrWhiteSpace(contentType))
            {
                throw new ArgumentNullException(nameof(contentType));
            }

            return new FileResult
            {
                FileName = fileName,
                ContentType = contentType
            };
        }

        protected virtual JavaScriptResult JavaScript(string script)
        {
            return new JavaScriptResult { Script = script };
        }

        protected virtual HttpNotFoundResult HttpNotFound(string statusDescription = null)
        {
            return new HttpNotFoundResult() { StatusDescription = statusDescription };
        }

        protected virtual HttpStatusCodeResult HttpStatusCode(HttpStatusCode statusCode, string statusDescription = null)
        {
            return HttpStatusCode((int)statusCode, statusDescription);
        }

        protected virtual HttpStatusCodeResult HttpStatusCode(int statusCode, string statusDescription = null)
        {
            return new HttpStatusCodeResult
            {
                StatusCode = statusCode,
                StatusDescription = statusDescription
            };
        }

        protected virtual JsonResult Json(object data, string contentType = null)
        {
            return new JsonResult
            {
                Data = data,
                ContentType = contentType
            };
        }

        protected virtual ViewResult View(object model = null, string folderName = null, string viewName = null)
        {
            viewName = String.IsNullOrWhiteSpace(viewName) ? ControllerContext.ActionDescriptor.ActionName : viewName;
            folderName = String.IsNullOrWhiteSpace(folderName) ? ControllerContext.ControllerDescriptor.ControllerName : folderName;

            return new ViewResult
            {
                FolderName=folderName,
                ViewName = viewName,
                Model = model
            };
        }

        // TODO:先暂时省略一些必要的属性与方法
    }
}
