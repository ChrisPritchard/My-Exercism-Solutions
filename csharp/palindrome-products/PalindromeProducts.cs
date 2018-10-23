using System;
using System.Collections.Generic;
using System.Linq;

public static class PalindromeProducts
{
    public static (int, IEnumerable<(int,int)>) Largest(int minFactor, int maxFactor)
        => Result(minFactor, maxFactor, false);

    public static (int, IEnumerable<(int,int)>) Smallest(int minFactor, int maxFactor)
        => Result(minFactor, maxFactor, true);

    public static (int, IEnumerable<(int,int)>) Result(int minFactor, int maxFactor, bool smallest)
    {
        if(minFactor >= maxFactor)
            throw new ArgumentException();

        var result = Palindromes(minFactor, maxFactor)
            .GroupBy(o => o.product)
            .OrderBy(o => smallest ? o.Key : -o.Key)
            .First();

        return (result.Key, result.Select(o => (o.f1, o.f2)));
    }

    private static IEnumerable<(int f1, int f2, int product)> Palindromes(int minFactor, int maxFactor)
    {
        IEnumerable<int> range() => Enumerable.Range(minFactor, maxFactor - minFactor + 1);
        
        var results =
            range().SelectMany(f1 => range().Select(f2 => 
            {
                var first = f1 < f2 ? f1 : f2;
                var second = f2 > f1 ? f2 : f1;
                return (f1: first, f2: second, product: f1 * f2);
            }))
            .Where(o => IsPalindrome(o.product))
            .Distinct();

        if(!results.Any())
            throw new ArgumentException();

        return results;
    }

    private static bool IsPalindrome(int number)
    {
        var reverse = 0;
        var original = number;

        while(number > 0)
        {
            reverse = reverse * 10 + number % 10;
            number /= 10;
        }

        return reverse == original;
    }
}
