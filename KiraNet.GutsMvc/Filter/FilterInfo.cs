namespace KiraNet.GutsMvc.Filter
{
    public class FilterInfo<T>
        where T:class
    {
        public T Filter { get; set; }
        public FilterScope Scope { get; set; }
        public int? Order { get; set; }

    }
}
