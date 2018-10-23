using System;
using System.Collections.Generic;
using System.Linq;

public static class NthPrime
{
    public static int Prime(int nth)
    {
        if(nth < 1)
            throw new ArgumentOutOfRangeException();

        return Primes().ElementAt(nth - 1);
    }

    private static IEnumerable<int> Primes()
    {
        var found = new List<int>();
        var i = 1;
        while(i++ > 0)
        {
            if(found.Any(p => i % p == 0))
                continue;
            
            yield return i;
            found.Add(i);
        }
    }
}