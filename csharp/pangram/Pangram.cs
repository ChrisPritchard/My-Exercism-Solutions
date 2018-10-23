using System;
using System.Linq;

public static class Pangram
{
    public static bool IsPangram(string input) =>
        "abcdefghijklmnopqrstuvwxyz".All(c => input.ToLower().Contains(c.ToString()));
}
