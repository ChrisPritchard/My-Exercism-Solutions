using System;
using System.Linq;
using System.Collections.Generic;

public static class WordCount
{
    private static readonly char[] splitChars = 
        new[] { ' ', ',', ':', '!', '&', '@', '$', '%', '^', '\n', '.' };

    public static IDictionary<string, int> CountWords(string phrase)
        => phrase.ToLower()
            .Split(splitChars, StringSplitOptions.RemoveEmptyEntries)
            .Select(w => w.Trim('\'', '"'))
            .GroupBy(o => o)
            .ToDictionary(o => o.Key, o => o.Count());
}