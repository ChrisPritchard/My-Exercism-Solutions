using System;
using System.Linq;

public class Anagram
{
    private readonly string original;
    private readonly string ordered;

    private static string SortWord(string word) => 
        string.Concat(word.ToLower().OrderBy(o => o));

    public Anagram(string baseWord) => 
        (original, ordered) = (baseWord.ToLower(), SortWord(baseWord));

    public string[] Anagrams(string[] potentialMatches) => 
        potentialMatches
            .Where(w => w.ToLower() != original && SortWord(w) == ordered)
            .ToArray();
}