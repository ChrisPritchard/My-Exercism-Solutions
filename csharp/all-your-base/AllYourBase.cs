using System;
using System.Linq;
using System.Collections.Generic;
using static System.Math;

public static class AllYourBase
{
    public static int[] Rebase(int inputBase, int[] inputDigits, int outputBase)
    {
        if(inputBase < 2 || outputBase < 2 || inputDigits.Any(n => n < 0 || n >= inputBase))
            throw new ArgumentException();

        var number = inputDigits.Reverse().Select((n, i) => 
            n * Pow(inputBase, i)).Sum();
        var result = new List<int>();

        while(number > 0)
        {
            var digit = number % outputBase;
            result.Insert(0, (int)digit);
            number = Math.Floor(number / outputBase);
        }

        return result.Count == 0 ? new[] { 0 } : result.ToArray();
    }
}