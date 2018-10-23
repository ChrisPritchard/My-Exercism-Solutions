using System;
using System.Linq;

public static class Grains
{
    public static ulong Square(int n)
    {
        if(n < 1 || n > 64)
            throw new ArgumentOutOfRangeException($"{nameof(n)} should be between 1 and 64 inclusive");
            
        return (ulong)Math.Pow(2, n - 1);
    }

    public static ulong Total() => Enumerable.Range(1, 64)
        .Select(Square)
        .Aggregate(0UL, (o, i) => o + i); // no .Sum for IEnum<ulong>
}