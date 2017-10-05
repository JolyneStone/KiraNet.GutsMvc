using KiraNet.GutsMVC.Route;
using System;

namespace KiraNet.GutsMVC
{
    /// <summary>
    /// 表示对当前HTTP请求上下文的抽象
    /// 注：我们能从HttpContext中获取当前请求的细节，同时也利用它来完成对请求的处理
    /// </summary>
    public abstract class HttpContext
    {
        public abstract IFeatureCollection HttpContextFeatures { get; }
        public abstract HttpRequest Request { get; internal set; }
        public abstract HttpResponse Response { get; internal set; }
        public abstract RouteContext Route { get; internal set; }
        public virtual RouteEntity RouteEntity
        {
            get
            {
                return Request.RouteEntity;
            }
            internal set
            {
                Request.RouteEntity = value;
            }
        }
        //public virtual RouteData RouteData
        //{
        //    get
        //    {
        //        return Request.RouteData;
        //    }
        //    internal set
        //    {
        //        Request.RouteData = value;
        //    }
        //}
        private Session _session;
        public Session Session
        {
            get
            {
                if (_session == null)
                {
                    _sessionManager = (_sessionManager ?? this.BuilderSessionManager());
                    if (_sessionManager.TryGetSession(out _session))
                    {
                        return _session;
                    }
                    else if (_sessionManager.TryCreateSession(out _session))
                    {
                        return _session;
                    }

                    return null;
                }

                return _session;
            }
        }
        public IServiceProvider ServiceRoot { get; protected set; }
        public IServiceProvider Service { get; protected set; }

        private ISessionManager _sessionManager;
        /// <summary>
        /// 是否取消对该请求的处理
        /// </summary>
        public bool IsCancel { get; set; } = false;

        //public abstract CancellationToken RequestAborted { get; set; }

        //public abstract void Abort();

        protected ISessionManager BuilderSessionManager(string sessionId = "", bool isCreateSession = false)
        {
            return _sessionManager ?? (_sessionManager = new SessionManager(this, SessionExpireTimeConfigure.ExpireConfigure, sessionId, isCreateSession));
        }
    }
}
