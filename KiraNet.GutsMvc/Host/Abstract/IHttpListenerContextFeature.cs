namespace KiraNet.GutsMvc
{
    public interface IHttpListenerContextFeature
    {
        IHttpRequestFeature RequestFeature { get; }
        IHttpResponseFeature ResponseFeature { get; }
        IWebSocketFeature WebSocketFeature { get; }
    }
}
