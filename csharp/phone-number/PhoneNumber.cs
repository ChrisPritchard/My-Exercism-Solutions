using System;
using System.Linq;

public class PhoneNumber
{
    private const int areaStart = 0;
    private const int exchangeStart = 3;
    private static char[] invalidStarts = new[] {'0', '1'};

    public static string Clean(string phoneNumber)
    {
        void invalid() => throw new ArgumentException($"{nameof(phoneNumber)} is not a valid phone number");;

        var noSeperators = string.Concat(phoneNumber.Split(new[] { ' ', '.', '-', '(', ')', '+' }));
        var noNonNumbers = string.Concat(noSeperators.Where(char.IsNumber));
        var noCountryCode = noNonNumbers.Length == 11 ? noNonNumbers.Substring(1) : noNonNumbers;

        if(noNonNumbers != noSeperators)
            invalid();
        else if(noNonNumbers.Length < 10 || noNonNumbers.Length > 11)
            invalid();
        else if(noNonNumbers.Length == 11 && !noNonNumbers.StartsWith("1"))
            invalid();
        else if(invalidStarts.Any(c => noCountryCode[areaStart] == c || noCountryCode[exchangeStart] == c))
            invalid();

        return noCountryCode;
    }    
}