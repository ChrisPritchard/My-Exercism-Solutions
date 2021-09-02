using System;

public class Octal
{
    public static int ToDecimal(string octal)
    {
        var sum = 0;
        for (int i = 0, j = octal.Length-1; i < octal.Length; i++, j--)
        {
            var c = octal[i];
            if (c < '0' || c > '7')
                return 0;
            sum += (c - '0') * (int)Math.Pow(8, j);
        }
        return sum;
    }
}
