using System;
using System.Collections.Generic;

public static class AccumulateExtensions
{
    public static IEnumerable<U> Accumulate<T, U>(this IEnumerable<T> collection, Func<T, U> func)
    {
        // avoiding the use of System.Linq.Select
        // which would have made this => collection.Select(func);
        foreach(var o in collection)
            yield return func(o);
    }
}