using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public static class IEnumerableExtensions
    {
        public static T Random<T>(this IEnumerable<T> enumerable, Random random)
        {
            return enumerable.ElementAt(random.Next(enumerable.Count()));
        }
    }
}
