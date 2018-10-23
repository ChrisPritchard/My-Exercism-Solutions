using System;
using System.Linq;

public static class Luhn
{
    public static bool IsValid(string number)
    {
        try
        {
            var results =
                number.Where(c => c != ' ').Reverse()
                .Select((c, i) => 
                {
                    if (!char.IsNumber(c))
                        throw new ArgumentException();
                    var value = int.Parse(c.ToString());
                    if(i % 2 == 1)
                        return value > 4 ? value * 2 - 9 : value * 2;
                    else 
                        return value;
                }).ToArray();
            return results.Length > 1 && results.Sum() % 10 == 0;
        }
        catch (ArgumentException)
        {
            return false;
        }
    }
}