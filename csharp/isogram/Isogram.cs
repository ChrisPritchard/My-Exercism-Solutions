using System;
using System.Linq;

public static class Isogram
{
    public static bool IsIsogram(string word)
        => word.ToLower().Where(char.IsLetter).GroupBy(o => o).All(o => o.Count() == 1);
}
