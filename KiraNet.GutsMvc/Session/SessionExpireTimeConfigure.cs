using System;

namespace KiraNet.GutsMVC
{
    public class SessionExpireTimeConfigure
    {
        public static TimeSpan ExpireConfigure { get; internal set; } = new TimeSpan(0, 20, 0);
    }
}
