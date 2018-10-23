using System;
using System.Linq;

public static class Series
{
    public static string[] Slices(string numbers, int sliceLength)
    {
        if(sliceLength < 1 || sliceLength > numbers.Length)
            throw new ArgumentException();
            
        return Enumerable
            .Range(0, numbers.Length - (sliceLength - 1))
            .Select(i => numbers.Substring(i, sliceLength))
            .ToArray();
    }
}