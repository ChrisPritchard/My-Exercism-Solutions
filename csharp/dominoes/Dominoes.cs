using System;
using System.Collections.Generic;
using System.Linq;

public static class Dominoes
{
    public static bool CanChain(IEnumerable<(int front, int back)> dominoes)
    {
        if (!dominoes.Any())
            return true;
        var start = dominoes.First();
        return TryChain(start.back, dominoes.Skip(1)) == start.front;
    }

    private static int? TryChain(int side, IEnumerable<(int front, int back)> dominoes)
    {
        if (!dominoes.Any())
            return side;

        IEnumerable<(int, int)> without((int, int) toExclude, IEnumerable<(int, int)> source)
        {
            var position = source.ToList().IndexOf(toExclude);
            return source.Take(position).Concat(source.Skip(position + 1));
        }

        return dominoes
            .Where(d => d.front == side || d.back == side)
            .Select(d => 
            {
                var exceptD = without(d, dominoes);
                return side == d.front 
                    ? TryChain(d.back, exceptD) 
                    : TryChain(d.front, exceptD);
            })
            .FirstOrDefault(d => d != null);
    }
}