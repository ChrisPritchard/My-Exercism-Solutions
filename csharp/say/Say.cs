using System;
using System.Linq;
using System.Collections.Generic;

public static class Say
{
    public static string InEnglish(long number)
    {
        if(number < 0 || number > Math.Pow(10, 12) - 1)
            throw new ArgumentOutOfRangeException();
        if (number == 0)
            return "zero";

        return UnderTrillion(number);
    }

    private static (long, long) Divide (long number, int power)
    { 
        var devisor = (long)Math.Pow(10.0, power);
        return (number / devisor, number % devisor);
    }

    private static readonly Dictionary<long, string> underTwentyNames = 
        new (long number, string name)[] 
        { (1, "one"), (2, "two"), (3, "three"), (4, "four"), (5, "five"),
          (6, "six"), (7, "seven"), (8, "eight"), (9, "nine"), (10, "ten"),
          (11, "eleven"), (12, "twelve"), (13, "thirteen"), (14, "fourteen"), (15, "fifteen"),
          (16, "sixteen"), (17, "seventeen"), (18, "eighteen"), (19, "nineteen") }
        .ToDictionary(o => o.number, o => o.name);

    private static readonly Dictionary<long, string> underHundredPrefixes = 
        new (long number, string name)[] 
        { (2, "twenty-"), (3, "thirty-"), (4, "forty-"), (5, "fifty-"),
          (6, "sixty-"), (7, "seventy-"), (8, "eighty-"), (9, "ninety-") }
        .ToDictionary(o => o.number, o => o.name);

    private static string UnderTwenty(long number)
        => underTwentyNames.GetValueOrDefault(number, "");

    private static string UnderHundred(long number)
    {
        var (tens, remainder) = Divide(number, 1);
        if (tens < 2)
            return UnderTwenty(number);

        var prefix = underHundredPrefixes.GetValueOrDefault(tens, "");
        var name = UnderTwenty(remainder);
        return $"{prefix}{name}".TrimEnd('-');
    }

    private static string UnderThousand(long number)
    {
        var (hundreds, remainder) = Divide(number, 2);
        if(hundreds == 0)
            return UnderHundred(number);
        return $"{UnderTwenty(hundreds)} hundred {UnderHundred(remainder)}".Trim();
    }

    private static string UnderMillion(long number)
    {
        var (thousands, remainder) = Divide(number, 3);
        if(thousands == 0)
            return UnderThousand(number);
        return $"{UnderThousand(thousands)} thousand {UnderThousand(remainder)}".Trim();
    }

    private static string UnderBillion(long number)
    {
        var (millions, remainder) = Divide(number, 6);
        if(millions == 0)
            return UnderMillion(number);
        return $"{UnderThousand(millions)} million {UnderMillion(remainder)}".Trim();
    }

    private static string UnderTrillion(long number)
    {
        var (billions, remainder) = Divide(number, 9);
        if(billions == 0)
            return UnderBillion(number);
        return $"{UnderThousand(billions)} billion {UnderBillion(remainder)}".Trim();
    }
}