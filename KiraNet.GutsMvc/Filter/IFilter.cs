namespace KiraNet.GutsMvc.Filter
{
    public interface IFilter
    {
        bool AllowMultiple { get; }
        int Order { get; }
    }
}
