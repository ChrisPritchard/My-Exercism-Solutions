using System;
using System.Linq;
using System.Collections.Generic;
using static System.Math;

public class Triplet
{
    private readonly int[] elems;

    public Triplet(int a, int b, int c) => elems = new[] { a, b, c };

    public int Sum() => elems.Sum();

    public int Product() => elems.Aggregate((p, o) => p * o);

    public bool IsPythagorean() => Pow(elems[0], 2) + Pow(elems[1], 2) == Pow(elems[2], 2);

    public static IEnumerable<Triplet> Where(int maxFactor, int minFactor = 1, int sum = 0)
    {
        for(var i = minFactor; i < maxFactor - 1; i++)
            for(var j = i + 1; j < maxFactor; j++)
                for(var k = j + 1; k <= maxFactor; k++)
                    {
                        var triplet = new Triplet(i, j, k);
                        if(sum != 0 && triplet.Sum() != sum)
                            continue;
                        if (triplet.IsPythagorean())
                            yield return triplet;
                    }
    }
}