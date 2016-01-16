using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class DefaultRandom
{
    private static Random rand = new Random();

    public static int Next()
    {
        return rand.Next();
    }

    public static int Next(int maxValue)
    {
        return rand.Next(maxValue);
    }

    public static int Next(int minValue, int maxValue)
    {
        return rand.Next(minValue, maxValue);
    }

    public static void NextBytes(byte[] buffer)
    {
        rand.NextBytes(buffer);
    }

    public static double NextDouble()
    {
        return rand.NextDouble();
    }
}
