using System;
using System.Linq;
using System.Collections.Generic;

public static class SumOfMultiples
{
    public static int Sum(IEnumerable<int> multiples, int max) =>
        multiples.SelectMany(o => 
            Enumerable.Range(0, max).Select(n => n * o).Where(n => n < max))
            .Distinct().Sum();
}