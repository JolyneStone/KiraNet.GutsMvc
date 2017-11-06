using System;
using System.Collections.Concurrent;
using System.Net;

namespace KiraNet.GutsMvc
{
    public class SessionManager : ISessionManager
    {
        private const string COOKIE_SESSION_ID = "gutsmvc_session";
        /// <summary>
        /// Session创建时引发
        /// </summary>
        public event EventHandler<SessionArgs> SessionCreating;
        /// <summary>
        /// Session创建后引发
        /// </summary>
        public event EventHandler<SessionArgs> SessionCreated;
        /// <summary>
        /// Session关闭时引发
        /// </summary>
        public event EventHandler<SessionArgs> SessionCloseing;
        /// <summary>
        /// Session关闭后引发
        /// </summary>
        public event EventHandler<SessionArgs> SessionClosed;

        private static ConcurrentDictionary<string, Session> _sessionDictionary = new ConcurrentDictionary<string, Session>();

        private HttpContext _context;

        public TimeSpan ExpireTime { get; set; } = SessionExpireTimeConfigure.ExpireConfigure; // 默认过期时间为20分钟

        internal SessionManager(HttpContext context, TimeSpan expireTime = default, string sessionId = "", bool isCreateSession = false)
        {
            _context = context;
            ExpireTime = expireTime == default ? ExpireTime : expireTime;

            Cookie cookie;
            if ((cookie = context.Request.Cookies[COOKIE_SESSION_ID]) != null)
            {
                sessionId = cookie.Value;
                if ((cookie = context.Response.Cookies[COOKIE_SESSION_ID]) != null)
                    return;

                TryCreateSession(sessionId, out var session);
            }

            if (isCreateSession)
            {
                if ((cookie = context.Response.Cookies[COOKIE_SESSION_ID]) == null)
                    return;

                TryCreateSession(sessionId, out var session);
            }

            //context.SessionManager = this;
        }

        public bool TryCreateSession(out Session session)
        {
            return TryCreateSession(String.Empty, out session);
        }
        public bool TryCreateSession(string sessionId, out Session session)
        {
            if (String.IsNullOrWhiteSpace(sessionId))
            {
                sessionId = Guid.NewGuid().ToString();
            }

            SessionCreating?.Invoke(this, new SessionArgs(null));
            Cookie cookie;
            if ((cookie = _context.Request.Cookies[COOKIE_SESSION_ID]) != null)
            {
                if (_sessionDictionary.TryGetValue(cookie.Value, out session))
                {
                    SessionCreated?.Invoke(this, new SessionArgs(session, true));
                    return true;
                }
                else
                {
                    session = new Session(sessionId, ExpireTime);
                    if (_sessionDictionary.TryAdd(session.Id, session))
                    {
                        //_context.Session = session;
                        SessionCreated?.Invoke(this, new SessionArgs(session, true));
                        return true;
                    }
                    else
                    {
                        SessionClosed?.Invoke(this, new SessionArgs(session, false));
                        return false;
                    }
                }
            }

            session = new Session(sessionId, ExpireTime);

            _context.Response.Cookies.Add(new Cookie(COOKIE_SESSION_ID, session.Id)
            {
                Expires = session.ExpireTime,
                Path="/"
            });
            if (_sessionDictionary.TryAdd(session.Id, session))
            {
                //_context.Session = session;
                SessionCreated?.Invoke(this, new SessionArgs(session, true));
                return true;
            }

            SessionClosed?.Invoke(this, new SessionArgs(session, false));
            return false;
        }

        //public bool TryCreateSession(Session session)
        //{
        //    SessionCreating?.Invoke(this, new SessionArgs(session));

        //    if (session == null)
        //        return false;
        //    if (String.IsNullOrWhiteSpace(session.Id))
        //        session.Id = Guid.NewGuid().ToString();

        //    if (session.ExpireTime == default(DateTime))
        //    {
        //        session.ExpireTime = DateTime.Now.AddMinutes(20);
        //    }

        //    _context.Response.Cookies.Add(new Cookie(COOKIE_SESSION_ID, session.Id) { Expires = session.ExpireTime });

        //    //_sessionDictionary.AddOrUpdate(session.Id, session, (x, y) => y);
        //    if (_sessionDictionary.TryAdd(session.Id, session))
        //    {
        //        _context.Session = session;
        //        SessionCreated?.Invoke(this, new SessionArgs(session, true));
        //        return true;
        //    }

        //    SessionClosed?.Invoke(this, new SessionArgs(session, false));
        //    return false;
        //}

        public bool TryRemoveSession(string sessionId)
        {
            SessionCloseing?.Invoke(this, new SessionArgs(new Session(sessionId)));
            if (_sessionDictionary.TryRemove(sessionId, out var session))
            {
                _context.Response.Cookies.Remove(sessionId);

                //_context.Session = null;
                SessionCreated?.Invoke(this, new SessionArgs(new Session(sessionId), true));
                return true;
            }

            SessionCreated?.Invoke(this, new SessionArgs(new Session(sessionId), false));
            return false;
        }

        public bool TryRemoveSession(Session session)
        {

            SessionCloseing?.Invoke(this, new SessionArgs(session));
            if (_sessionDictionary.TryRemove(session.Id, out var value))
            {
                _context.Response.Cookies.Remove(session.Id);
                session = null;

                //_context.Session = null;
                SessionClosed?.Invoke(this, new SessionArgs(value, true));
                return true;
            }

            session = value;
            //_context.Session = value;
            SessionClosed?.Invoke(this, new SessionArgs(value, false));
            return false;
        }

        public bool TryRemoveSession()
        {
            Cookie cookie;
            if ((cookie = _context.Response.Cookies[COOKIE_SESSION_ID]) == null)
                return false;


            SessionCloseing?.Invoke(this, new SessionArgs(null));
            if (_sessionDictionary.TryRemove(cookie.Name, out var session))
            {
                _context.Response.Cookies.Remove(cookie.Name);
                //_context.Session = null;
                SessionClosed?.Invoke(this, new SessionArgs(session, true));
                return true;
            }

            SessionClosed?.Invoke(this, new SessionArgs(session, false));
            return false;
        }

        public bool TryGetSession(string sessionId, out Session session)
        {
            session = null;
            if (String.IsNullOrWhiteSpace(sessionId))
            {
                return false;
            }

            Cookie cookie;
            if ((cookie = _context.Request.Cookies[COOKIE_SESSION_ID]) != null)
            {
                if (!cookie.Expired && _sessionDictionary.TryGetValue(sessionId, out session))
                {
                    session.UpdateTime();
                    cookie.Expires = session.ExpireTime;
                    //_context.Session = session;
                    return true;
                }

                return false;
            }

            return false;
        }

        public bool TryGetSession(out Session session)
        {
            session = null;

            Cookie cookie;
            if ((cookie = _context.Request.Cookies[COOKIE_SESSION_ID]) != null)
            {
                if (!cookie.Expired && _sessionDictionary.TryGetValue(cookie.Value, out session))
                {
                    session.UpdateTime();
                    cookie.Expires = session.ExpireTime;
                    //_context.Session = session;
                    return true;
                }

                return false;
            }

            return false;
        }

        /// <summary>
        /// 回收Session或更新Session过期时间（滑动过期）
        /// </summary>
        public void RecycleOrSet()
        {
            Cookie cookie;
            if ((cookie = _context.Request.Cookies[COOKIE_SESSION_ID]) == null)
            {
                return;
            }

            if (!_sessionDictionary.TryGetValue(cookie.Name, out var session))
            {
                return;
            }

            if (session.IsAbsolute) // 绝对过期策略
            {
                if (session.ExpireTime >= DateTime.Now)
                {
                    _sessionDictionary.TryRemove(session.Id, out var _);
                    //_context.Session = null;
                }
            }
            else
            {
                if (session.ExpireTime < DateTime.Now)
                {
                    session.UpdateTime();
                    //_context.Session = session;
                }
                else
                {
                    _sessionDictionary.TryRemove(session.Id, out var _);
                    //_context.Session = null;
                }
            }
        }
    }
}