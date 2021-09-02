using System;

public class Hexadecimal
{
    public static int ToDecimal(string hexadecimal)
    {
        var sum = 0;
        for (int i = 0, j = hexadecimal.Length-1; i < hexadecimal.Length; i++, j--)
        {
            var c = hexadecimal[i];
            if ((c < '0' || c > '9') && (c < 'a' || c > 'f'))
                return 0;
            var value = c - '0';
            if (c >= 'a')
                value = 10 + c - 'a';
            sum += value * (int)Math.Pow(16, j);
        }
        return sum;
    }
}
