using System;
using System.Collections.Generic;

namespace KiraNet.GutsMVC
{
    internal static class CollectionExtensions
    {
        public static T[] ToArrayWithoutNulls<T>(this ICollection<T> collection) where T : class
        {
            if (collection == null)
                return null;

            T[] result = new T[collection.Count];
            int count = 0;
            foreach(T value in collection)
            {
                if(value!= null)
                {
                    result[count] = value;
                    count++;
                }
            }

            if(count == collection.Count)
            {
                return result;
            }

            T[] tempReult = new T[count];
            // 复制数组的用意是为了能够防止引用的干扰
            Array.Copy(result, tempReult, count);
            return tempReult;
        }
    }
}
