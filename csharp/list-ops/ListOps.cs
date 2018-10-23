using System;
using System.Collections.Generic;

public static class ListOps
{
    private static List<T> Cons<T>(T newHead, List<T> list)
    {
        list.Insert(0, newHead);
        return list;
    }

    public static int Length<T>(List<T> input)
        => Foldl(input, 0, (count, o) => count + 1);

    public static List<T> Reverse<T>(List<T> input)
        => Foldl(input, new List<T>(), (state, o) => Cons(o, state));

    public static List<TOut> Map<TIn, TOut>(List<TIn> input, Func<TIn, TOut> map)
        => Foldr(input, new List<TOut>(), (o, state) => Cons(map(o), state));

    public static List<T> Filter<T>(List<T> input, Func<T, bool> predicate)
        => Foldr(input, new List<T>(), (o, state) => predicate(o) ? Cons(o, state) : state);

    public static TOut Foldl<TIn, TOut>(List<TIn> input, TOut start, Func<TOut, TIn, TOut> func)
    {
        var current = start;
        foreach(var o in input)
            current = func(current, o);
        return current;
    }

    public static TOut Foldr<TIn, TOut>(List<TIn> input, TOut start, Func<TIn, TOut, TOut> func)
        => Foldl(Reverse(input), start, (o, state) => func(state, o));

    public static List<T> Concat<T>(List<List<T>> input)
        => Foldl(input, new List<T>(), (state, o) => Append(state, o));

    public static List<T> Append<T>(List<T> left, List<T> right)
        => Foldr(left, right, Cons);
}