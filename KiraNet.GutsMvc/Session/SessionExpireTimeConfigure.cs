using System;

namespace KiraNet.GutsMvc
{
    public class SessionExpireTimeConfigure
    {
        public static TimeSpan ExpireConfigure { get; internal set; } = new TimeSpan(0, 20, 0);
    }
}
