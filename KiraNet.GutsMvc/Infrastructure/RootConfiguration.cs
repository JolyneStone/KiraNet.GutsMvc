using System;

namespace KiraNet.GutsMvc.Infrastructure
{
    public class RootConfiguration
    {
        internal static void InitialRoot(string root)
        {
            if (String.IsNullOrWhiteSpace(Root))
            {
                Root = root;
            }
        }

        public static string Root { get; private set; }
    }
}
