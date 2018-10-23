using System;
using System.Collections.Generic;
using System.Linq;

public static class ParallelLetterFrequency
{
    public static Dictionary<char, int> Calculate(IEnumerable<string> texts) =>
        texts.AsParallel()
            .SelectMany(o => 
                o.ToLower()
                .GroupBy(c => c)
                .Where(c => char.IsLetter(c.Key))
                .Select(c => (character: c.Key, count: c.Count())))
            .GroupBy(c => c.character)
            .Select(c => (character: c.Key, count: c.Select(ci => ci.count).Sum()))
            .ToDictionary(c => c.character, c => c.count);
}