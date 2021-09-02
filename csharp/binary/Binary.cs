using System;

public class Binary
{
    public static int ToDecimal(string binary)
    {
        var sum = 0;
        for (int i = 0, j = binary.Length-1; i < binary.Length; i++, j--)
        {
            var c = binary[i];
            if (c < '0' || c > '1')
                return 0;
            sum += (c - '0') * (int)Math.Pow(2, j);
        }
        return sum;
    }
}
