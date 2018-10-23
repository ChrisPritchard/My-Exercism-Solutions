using System;
using System.Linq;
using System.Collections.Generic;

public static class Sieve
{
    public static int[] Primes(int limit)
    {
        if(limit < 2)
            throw new ArgumentOutOfRangeException($"{nameof(limit)} must be greater than or equal to 2");
        
        var numbers = Enumerable.Range(2, limit - 1);
        var primes = new List<int>();
        while(numbers.Any())
        {
            var newPrime = numbers.First();
            primes.Add(newPrime);
            numbers = numbers.Where(o => o % newPrime != 0);
        }

        return primes.ToArray();
    }
}