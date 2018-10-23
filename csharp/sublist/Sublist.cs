using System;
using System.Linq;
using System.Collections.Generic;

public enum SublistType
{
    Equal, Unequal, Superlist, Sublist
}

public static class Sublist
{
    public static SublistType Classify<T>(List<T> list1, List<T> list2)
        where T : IComparable
    {
        if(AreEqual(list1, list2))
            return SublistType.Equal;
        if(list1.Count > list2.Count && IsSublist(list2, list1))
            return SublistType.Superlist;
        if(list2.Count > list1.Count && IsSublist(list1, list2))
            return SublistType.Sublist;
        return SublistType.Unequal;
    }

    private static bool AreEqual<T>(List<T> list1, List<T> list2)
        where T : IComparable =>
        list1.Count == list2.Count && list1
            .Select((item, i) => new { item, i })
            .All(o => o.item.Equals(list2[o.i]));

    private static IEnumerable<List<T>> Windowed<T>(int size, List<T> list)
        => Enumerable.Range(0, list.Count - size + 1)
            .Select(i => list.Skip(i).Take(size).ToList())
            .Append(new List<T>());

    private static bool IsSublist<T>(List<T> child, List<T> parent)
        where T : IComparable => 
        Windowed(child.Count, parent).Any(sl => AreEqual(sl, child));
}