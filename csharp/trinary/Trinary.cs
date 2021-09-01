using System;

public class Trinary
{
    public static int ToDecimal(string trinary)
    {
        var sum = 0;
        for (var i = 0; i < trinary.Length; i++)
        {
            var c = trinary[i];
            if (c < '0' || c > '2')
                return 0;
            sum += (c - '0') * (int)Math.Pow(3, (trinary.Length - 1) - i);
        }
        return sum;
    }
}
