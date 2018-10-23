using System;
using System.Linq;

public static class Proverb
{
    public static string[] Recite(string[] subjects)
    { 
        if (subjects.Length == 0)
            return new string[0];

        return subjects
            .Zip(subjects.Skip(1), (first, second) => (first, second))
            .Select(pair => $"For want of a {pair.first} the {pair.second} was lost.")
            .Append($"And all for the want of a {subjects[0]}.").ToArray();
    }
}