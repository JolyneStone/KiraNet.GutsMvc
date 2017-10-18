using System;
using System.Collections;
using System.Collections.Generic;

namespace KiraNet.GutsMvc
{
    public sealed class Session : IEnumerable<KeyValuePair<string, object>>
    {
        /// <summary>
        /// 获取SessionId
        /// </summary>
        public string Id { get; internal set; }
        /// <summary>
        /// 获取Session过期时间
        /// </summary>
        public DateTime ExpireTime { get; internal set; }
        public bool IsAbsolute { get; internal set; } = true;

        private TimeSpan _span;

        private SessionManager _manager;

        private Dictionary<string, object> _sessions = new Dictionary<string, object>();
        internal Session(SessionManager manager)
        {
            _manager = manager;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="expireTime"></param>
        /// <param name="isAbsolute">过期策略：true，绝对过期；false，滑动过期</param>
        internal Session(string sessionId, TimeSpan expireTime, bool isAbsolute = true)
        {
            if (String.IsNullOrWhiteSpace(sessionId))
                Id = Guid.NewGuid().ToString();
            else
                Id = sessionId;

            IsAbsolute = isAbsolute;
            ExpireTime = DateTime.Now.Add(expireTime);
            _span = expireTime;
        }

        internal Session(TimeSpan expireTime, bool isAbsolute = true)
        {
            Id = Guid.NewGuid().ToString();

            IsAbsolute = isAbsolute;
            ExpireTime = DateTime.Now.Add(expireTime);
        }

        internal Session(string sessionId)
        {
            Id = sessionId;
        }

        public void Cancel()
            => _manager.TryRemoveSession(Id);

        /// <summary>
        /// 更新时间，滑动过期有效
        /// </summary>
        public void UpdateTime()
        {
            if (IsAbsolute)
                return;

            ExpireTime = DateTime.Now.Add(_span);
        }

        public object this[string key]
        {
            get
            {
                return this.Get(key);
            }
            set
            {
                this.AddOrSet(key, value);
            }
        }

        public void AddOrSet(string key, object value)
        {
            if (_sessions.ContainsKey(key))
                _sessions[key] = value;

            _sessions.Add(key, value);
        }

        public void Remove(string key)
        {
            if (_sessions[key] != null)
                _sessions.Remove(key);
        }

        public void Clear()
        {
            _sessions.Clear();
        }

        public object Get(string key)
        {
            if (_sessions.TryGetValue(key, out var value))
                return value;

            return null;
        }

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return ((IEnumerable<KeyValuePair<string, object>>)_sessions).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<KeyValuePair<string, object>>)_sessions).GetEnumerator();
        }
    }
}
