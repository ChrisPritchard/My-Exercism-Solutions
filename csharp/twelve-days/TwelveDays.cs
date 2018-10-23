using System;
using System.Linq;

public static class TwelveDays
{
    private static string Start(string day) => $"On the {day} day of Christmas my true love gave to me, ";

    private static readonly (string day, string present)[] lines = new[]
    {
        ("first", "a Partridge in a Pear Tree"),
        ("second", "two Turtle Doves"),
        ("third", "three French Hens"),
        ("fourth", "four Calling Birds"),
        ("fifth", "five Gold Rings"),
        ("sixth", "six Geese-a-Laying"),
        ("seventh", "seven Swans-a-Swimming"),
        ("eighth", "eight Maids-a-Milking"),
        ("ninth", "nine Ladies Dancing"),
        ("tenth", "ten Lords-a-Leaping"),
        ("eleventh", "eleven Pipers Piping"),
        ("twelfth", "twelve Drummers Drumming"),
    };

    public static string Recite(int verseNumber)
    {
        if(verseNumber < 1 || verseNumber > 12)
            throw new ArgumentException($"{nameof(verseNumber)} should be in the range 1 to 12");
        
        string joiner((string day, string _) verse) => 
            verse.day == "first" ? "." : $", " + (verse.day == "second" ? "and " : "");

        return Enumerable.Range(1, verseNumber).Reverse()
            .Select(i => lines[i - 1])
            .Aggregate(Start(lines[verseNumber - 1].day), (result, verse) => 
                result + $"{verse.present}{joiner(verse)}");
    }

    public static string Recite(int startVerse, int endVerse)
        => string.Join('\n', Enumerable.Range(startVerse, endVerse - startVerse + 1)
            .Select(i => Recite(i)));
}