using System;
using System.Collections.Generic;

public static class Diamond
{
    public static string Make(char target)
    {
        var rows = new List<string>();

        var letters = target - 'A' + 1;
        var rowLength = letters * 2 - 1;

        for(var row = 0; row < letters; row ++)
        {
            var letter = (char)('A' + row);
            var marginSpace = new string(' ', letters - row - 1);
            var innerSpace = new string(' ', rowLength - (marginSpace.Length * 2 + (row == 0 ? 1 : 2)));
            var diamondStrip = innerSpace.Length == 0 ? letter.ToString() : $"{letter}{innerSpace}{letter}";
            rows.Add($"{marginSpace}{diamondStrip}{marginSpace}");
        }

        for(var row = 1; row < letters; row ++)
        {
            var letter = (char)(target - row);
            var marginSpace = new string(' ', row);
            var innerSpace = new string(' ', rowLength - (marginSpace.Length * 2 + (row == letters - 1 ? 1 : 2)));
            var diamondStrip = innerSpace.Length == 0 ? letter.ToString() : $"{letter}{innerSpace}{letter}";
            rows.Add($"{marginSpace}{diamondStrip}{marginSpace}");
        }

        return string.Join('\n', rows);
    }
}