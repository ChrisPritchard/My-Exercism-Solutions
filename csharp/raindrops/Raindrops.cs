using System;
using System.Linq;
using System.Collections.Generic;

public static class Raindrops
{
    private static readonly (int, string)[] words = new []
    { (3, "Pling"), (5, "Plang"), (7, "Plong") };

    public static string Convert(int number)
    {
        var result = string.Concat(words.Select(option => 
        {
            var (num, text) = option;
            return number % num == 0 ? text : "";
        }));
        return string.IsNullOrWhiteSpace(result) ? number.ToString() : result;
    }
}