namespace KiraNet.GutsMvc.Filter
{
    /// <summary>
    /// Filter对象是对过滤器的封装
    /// </summary>
    public class Filter
    {
        public const int DefaultOrder = -1;
        public Filter(object instance, FilterScope scope, int? order)
        {
            Instance = instance;
            Scope = scope;
            Order = order ?? DefaultOrder;
        }

        public object Instance { get; }

        /// <summary>
        /// 对于过滤器的执行顺序
        /// 以Order属性为主，Scope属性为辅
        /// 规则都为数值越小优先级越高
        /// </summary>
        public int Order { get; }
        public FilterScope Scope { get; }
    }
}
