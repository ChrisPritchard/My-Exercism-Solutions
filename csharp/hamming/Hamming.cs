using System;
using System.Linq;

public static class Hamming
{
    public static int Distance(string firstStrand, string secondStrand)
    {
        if(firstStrand.Length != secondStrand.Length)
            throw new ArgumentException($"{nameof(firstStrand)} must be the same length as {nameof(secondStrand)}");

        return firstStrand.Where((c, i) => secondStrand[i] != c).Count();
    }
}