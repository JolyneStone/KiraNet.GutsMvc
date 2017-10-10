using System;

namespace KiraNet.GutsMvc
{
    public interface ISessionManager
    {
        bool TryGetSession(out Session session);
        bool TryCreateSession(out Session session);
        bool TryRemoveSession();
        void RecycleOrSet();
    }
}