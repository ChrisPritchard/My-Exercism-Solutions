using System;
using System.Linq;

public static class LargestSeriesProduct
{
    public static long GetLargestProduct(string digits, int span) 
    {
        if(span < 0 || span > digits.Length || digits.Any(o => !char.IsNumber(o)))
            throw new ArgumentException();

        return Enumerable
            .Range(0, digits.Length - (span - 1))
            .Select(i => digits
                .Substring(i, span)
                .Select(c => int.Parse(c.ToString()))
                .Aggregate(1, (p, o) => p * o))
            .Max();
    }
}