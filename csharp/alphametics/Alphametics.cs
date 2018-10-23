using System;
using System.Linq;
using System.Collections.Generic;
using Sprache;

public static class Alphametics
{
    public static IDictionary<char, int> Solve(string equation)
    {
        var allAdded = all.Parse(equation);
        var allChars = allAdded.added.Append(allAdded.summed).SelectMany(o => o).Distinct();
        var cantBeZero = allAdded.added.Append(allAdded.summed).Select(o => o.First()).Distinct();

        var result = Combinations(allChars, Enumerable.Empty<int>(), cantBeZero)
            .Select(o => o.ToDictionary(m => m.letter, m => m.value))
            .FirstOrDefault(m => IsValid(allAdded.added, allAdded.summed, m));

        return result ?? throw new ArgumentException();
    }

    private static IEnumerable<IEnumerable<(char letter, int value)>> Combinations (IEnumerable<char> remaining, IEnumerable<int> used, IEnumerable<char> cantBeZero)
    {
        if(!remaining.Any())
        {
            yield return Enumerable.Empty<(char letter, int value)>();
            yield break;
        }

        var letter = remaining.First();
        var start = cantBeZero.Contains(letter) ? 1 : 0;

        foreach(var n in Enumerable.Range(start, 10 - start).Except(used))
        {
            var option = (letter, n);
            foreach(var next in Combinations(remaining.Skip(1), used.Append(n), cantBeZero))
                yield return next.Append(option);
        }
    }

    private static bool IsValid(char[][] added, char[] summed, Dictionary<char, int> mapping)
    {
        long asNumber(char[] chars) => 
            chars.Reverse()
                .Select((c, i) => mapping.GetValueOrDefault(c) * (long)Math.Pow(10, i))
                .Sum();
        
        var targetNumber = asNumber(summed);
        long sum = 0;
        foreach(var chars in added)
        {
            var number = asNumber(chars);
            if(number > targetNumber || sum + number > targetNumber)
                return false;
            sum += number;
        }
        return sum == targetNumber;
    }

    private static Parser<char[]> added = 
        from numbers in Parse.AtLeastOnce(Parse.Letter) 
        from _ in Parse.Optional(Parse.WhiteSpace.Then(_ => Parse.Char('+')).Then(_ => Parse.WhiteSpace)) 
        select numbers.ToArray();

    private static Parser<char[]> summed = 
        from _ in Parse.WhiteSpace.Then(_ => Parse.String("==")).Then(_ => Parse.WhiteSpace) 
        from numbers in Parse.AtLeastOnce(Parse.Letter) 
        select numbers.ToArray();

    private static Parser<(char[][] added, char[] summed)> all =
        from allAdded in Parse.Many(added)
        from result in summed
        select (allAdded.ToArray(), result);
}