using System;
using System.Net;

namespace KiraNet.GutsMvc
{
    public static class CookieCollectionExtensions
    {
        public static void Remove(this CookieCollection cookieCollection, Cookie removeCookie)
        {
            Remove(cookieCollection, removeCookie.Name);
        }

        public static void Remove(this CookieCollection cookieCollection, string cookieName)
        {
            var cookie = cookieCollection[cookieName];
            if (cookie == null)
                return;

            cookie.Discard = true;
            cookie.Expires = DateTime.Now.AddHours(-1);
        }

        public static Cookie Get(this CookieCollection cookieCollection, string cookieName)
        {
            return cookieCollection[cookieName];
        }
    }
}
