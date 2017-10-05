//using System;
//using System.Collections.Generic;

//namespace KiraNet.GutsMVC
//{
//    public struct StringComparer
//    {
//        public static IEqualityComparer<string> Ordinal => new StringComparerOrdinal();
//        public static IEqualityComparer<string> OrdinalIgnoreCase => new StringComparerOrdinalIgnoreCase();
//        public static IEqualityComparer<string> CurrentCulture => new StringComparerCurrentCulture();
//        public static IEqualityComparer<string> CultureIgnoreCase => new StringComparerCultureIgnoreCase();
//        public static IEqualityComparer<string> InvariantCulture => new StringComparerInvariantCulture();
//        public static IEqualityComparer<string> InvariantCultureIgnoreCase => new StringComparerInvariantCultureIgnoreCase();
//    }

//    public class StringComparerOrdinalIgnoreCase : IEqualityComparer<string>
//    {
//        public bool Equals(string x, string y)
//            => String.Equals(x, y, StringComparison.OrdinalIgnoreCase);

//        public int GetHashCode(string obj)
//            => obj.GetHashCode();
//    }

//    public class StringComparerOrdinal : IEqualityComparer<string>
//    {
//        public bool Equals(string x, string y)
//            => String.Equals(x, y, StringComparison.Ordinal);

//        public int GetHashCode(string obj)
//            => obj.GetHashCode();
//    }

//    public class StringComparerInvariantCultureIgnoreCase : IEqualityComparer<string>
//    {
//        public bool Equals(string x, string y)
//            => String.Equals(x, y, StringComparison.InvariantCultureIgnoreCase);

//        public int GetHashCode(string obj)
//            => obj.GetHashCode();
//    }

//    public class StringComparerInvariantCulture : IEqualityComparer<string>
//    {
//        public bool Equals(string x, string y)
//            => String.Equals(x, y, StringComparison.InvariantCulture);

//        public int GetHashCode(string obj)
//            => obj.GetHashCode();
//    }

//    public class StringComparerCultureIgnoreCase : IEqualityComparer<string>
//    {
//        public bool Equals(string x, string y)
//            => String.Equals(x, y, StringComparison.CurrentCultureIgnoreCase);

//        public int GetHashCode(string obj)
//            => obj.GetHashCode();
//    }

//    public class StringComparerCurrentCulture : IEqualityComparer<string>
//    {
//        public bool Equals(string x, string y)
//            => String.Equals(x, y, StringComparison.CurrentCulture);

//        public int GetHashCode(string obj)
//            => obj.GetHashCode();
//    }
//}
