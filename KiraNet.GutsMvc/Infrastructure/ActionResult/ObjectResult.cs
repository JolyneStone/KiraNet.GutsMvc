//using KiraNet.GutsMvc.Implement;
//using Microsoft.Extensions.DependencyInjection;
//using System;
//using System.Threading.Tasks;

//namespace KiraNet.GutsMvc
//{
//    public class ObjectResult : ActionResult
//    {
//        public ObjectResult(object value)
//        {
//            Value = value;
//            ValueType = value.GetType();
//        }

//        public object Value { get; set; }

//        public Type ValueType { get; set; }

//        public int? StatusCode { get; set; }

//        public override Task ExecuteResultAsync(ActionContext context)
//        {
//            var executor = context.HttpContext.Service.GetRequiredService<ObjectResultExecutor>();
//            var result = executor.ExecuteAsync(context, this);

//            return result;
//        }

//        /// <summary>
//        /// This method is called before the formatter writes to the output stream.
//        /// </summary>
//        public virtual void OnFormatting(ActionContext context)
//        {
//            if (context == null)
//            {
//                throw new ArgumentNullException(nameof(context));
//            }

//            if (StatusCode.HasValue)
//            {
//                context.HttpContext.Response.StatusCode = StatusCode.Value;
//            }
//        }
//    }
//}
