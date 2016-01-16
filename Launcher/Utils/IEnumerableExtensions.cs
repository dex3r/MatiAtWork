using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public static class IEnumerableExtensions
{
    public static T Random<T>(this IEnumerable<T> enumerable, Random random)
    {
        return enumerable.ElementAt(random.Next(enumerable.Count()));
    }

    public static T RandomOrDefault<T>(this IEnumerable<T> enumerable, Random random)
    {
        int enumCount = enumerable.Count();

        if (enumCount == 0)
        {
            return default(T);
        }

        return enumerable.ElementAt(random.Next(enumCount));
    }

    /// <summary>
    /// Randomizes one element from this and given additional collections.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="enumerable"></param>
    /// <param name="random"></param>
    /// <param name="additionalEnumerables"></param>
    /// <returns></returns>
    public static T Random<T>(this IEnumerable<T> enumerable, Random random, params IEnumerable<T>[] additionalEnumerables)
    {
        int allEnumsCount = enumerable.Count();

        if (additionalEnumerables.Length > 0)
        {
            allEnumsCount += additionalEnumerables.Aggregate(0, (acc, x) => acc += x.Count());
        }

        int result = random.Next(allEnumsCount);

        if (additionalEnumerables.Length == 0 || result < enumerable.Count())
        {
            return enumerable.ElementAt(result);
        }
        else
        {
            int currentIndex = enumerable.Count();

            for (int i = 0; i < additionalEnumerables.Length; i++)
            {
                int interIndex = result - currentIndex;

                if (interIndex < additionalEnumerables[i].Count())
                {
                    return additionalEnumerables[i].ElementAt(interIndex);
                }

                currentIndex += additionalEnumerables[i].Count();
            }
        }

        throw new Exception("This should never happen. This method logic is all wrong.");
    }

    public static bool IsEmpty<T>(this IEnumerable<T> enumerable)
    {
        return !enumerable.GetEnumerator().MoveNext();
    }
}
