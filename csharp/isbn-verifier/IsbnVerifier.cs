using System;
using System.Linq;

public static class IsbnVerifier
{
    public static bool IsValid(string number)
    {
        if(string.IsNullOrWhiteSpace(number))
            return false;

        var check = number[number.Length - 1];
        if(check != 'X' && !char.IsNumber(check))
            return false;

        var clean = number.Substring(0, number.Length - 1).Where(c => c != '-');
        if(!clean.All(char.IsNumber) || clean.Count() != 9)
            return false;

        return clean
            .Select(c => int.Parse(c.ToString()))
            .Append(check == 'X' ? 10 : int.Parse(check.ToString()))
            .Reverse().Select((n, i) => n * (i + 1))
            .Sum() % 11 == 0;
    }
}