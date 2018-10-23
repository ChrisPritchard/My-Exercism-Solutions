using System;
using System.Linq;

public static class ScrabbleScore
{
    private static readonly (char[], int)[] scoreTable = new[]
    {
        (new[] {'A', 'E', 'I', 'O', 'U', 'L', 'N', 'R', 'S', 'T'}, 1),
        (new[] {'D', 'G'}, 2),
        (new[] {'B', 'C', 'M', 'P'}, 3),
        (new[] {'F', 'H', 'V', 'W', 'Y'}, 4),
        (new[] {'K'}, 5),
        (new[] {'J', 'X'}, 8),
        (new[] {'Q', 'Z'}, 10)
    };

    private static int FindScore(char letter)
    {
        var (_, value) = scoreTable.Single(o => 
        {
            var (letters, _) = o;
            return letters.Contains(char.ToUpper(letter));
        });
        return value;
    }

    public static int Score(string input) => input.Select(FindScore).Sum();
}