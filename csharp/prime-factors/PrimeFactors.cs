using System;
using System.Linq;
using System.Collections.Generic;

public static class PrimeFactors
{
    public static int[] Factors(long number)
    {
        var result = new List<int>();
        var possible = 2;
        
        while(number > 1 && possible <= number)
        {
            if(number % possible != 0)
                possible += 1;
            else
            {
                result.Add(possible);
                number = number / possible;
            }
        }

        return result.ToArray();
    }
}