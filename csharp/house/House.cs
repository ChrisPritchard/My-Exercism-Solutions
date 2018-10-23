using System;
using System.Linq;

public static class House
{
    private static readonly (string thing, string did)[] verses = new[]
    {
        ("",""),
        ("house that Jack built.", ""),
        ("malt", "lay in"), 
        ("rat", "ate"), 
        ("cat", "killed"), 
        ("dog", "worried"),
        ("cow with the crumpled horn", "tossed"), 
        ("maiden all forlorn", "milked"),
        ("man all tattered and torn", "kissed"), 
        ("priest all shaven and shorn", "married"),
        ("rooster that crowed in the morn", "woke"), 
        ("farmer sowing his corn", "kept"),
        ("horse and the hound and the horn", "belonged to")
    };

    public static string Recite(int verseNumber)
    {
        if(verseNumber < 1 || verseNumber > 12)
            throw new ArgumentException(nameof(verseNumber));

        var result = "";
        for(var i = verseNumber; i > 0; i--)
            result += i == verseNumber
                ? $"This is the {verses[i].thing}"
                : $" that {verses[i + 1].did} the {verses[i].thing}";
        return result;
    }

    public static string Recite(int startVerse, int endVerse) =>
        string.Join("\n", 
            Enumerable
                .Range(startVerse, endVerse - startVerse + 1)
                .Select(i => Recite(i)));
}