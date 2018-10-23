using System;
using System.Collections.Generic;
using System.Linq;

public static class VariableLengthQuantity
{
    public static uint[] Encode(uint[] numbers) => numbers.SelectMany(n => AsVLQ(n)).ToArray();

    private static IEnumerable<uint> AsVLQ(uint source, bool isFirst = true)
    {
        if(source > 127)
            foreach(var next in AsVLQ(source >> 7, false))
                yield return next;

        var result = source & 127;
        yield return isFirst ? result : result + 128;
    }

    public static uint[] Decode(uint[] bytes) => FromVLQ(bytes).ToArray();

    private static IEnumerable<uint> FromVLQ(uint[] bytes)
    {
        if(bytes.Last() > 127)
            throw new InvalidOperationException();

        uint sum = 0;
        foreach(var val in bytes)
            if(val > 127)
            {
                sum = sum << 7;
                sum += val - 128;
            }
            else
            {
                sum = sum << 7;
                yield return sum + val;
                sum = 0;   
            }
    }
}