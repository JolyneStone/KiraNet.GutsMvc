using Microsoft.Extensions.Primitives;
using System;

namespace KiraNet.GutsMVC
{
    public static class StringValuesExtensions
    {
        public static string ContvertToString(this StringValues stringValues)
        {
            return String.Join("|", stringValues.ToArray());
        }
    }
}
