using System;
using System.Collections.Generic;
using System.Linq;

public static class FoodChain
{
    private static readonly (string thing, string remark)[] things = new []
    {
        ("",""),
        ("fly", "I don't know why she swallowed the fly. Perhaps she'll die."),
        ("spider", "It wriggled and jiggled and tickled inside her."),
        ("bird", "How absurd to swallow a bird!"),
        ("cat", "Imagine that, to swallow a cat!"),
        ("dog", "What a hog, to swallow a dog!"),
        ("goat", "Just opened her throat and swallowed a goat!"),
        ("cow", "I don't know how she swallowed a cow!"),
        ("horse", "She's dead, of course!")
    };

    public static string Recite(int verseNumber)
    {
        var lines = new List<string>
        {
            $"I know an old lady who swallowed a {things[verseNumber].thing}.",
            things[verseNumber].remark
        };

        if(verseNumber == things.Length - 1 || verseNumber == 1)
            return string.Join("\n", lines);

        for(var i = verseNumber - 1; i > 0; i--)
        {
            var targetThing = i != 2 ? things[i].thing + "." 
                : $"{things[i].thing} that {things[i].remark.Replace("It ", "")}";
            lines.Add($"She swallowed the {things[i + 1].thing} to catch the {targetThing}");
        }

        lines.Add(things[1].remark);
        return string.Join("\n", lines);
    }

    public static string Recite(int startVerse, int endVerse) =>
        string.Join("\n\n", Enumerable.Range(startVerse, endVerse - startVerse + 1)
            .Select(verse => Recite(verse)));
}