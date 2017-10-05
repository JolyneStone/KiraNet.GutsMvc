namespace KiraNet.GutsMVC
{
    public interface IHttpListenerContextFeature
    {
        IHttpRequestFeature RequestFeature { get; }
        IHttpResponseFeature ResponseFeature { get; }
    }
}
