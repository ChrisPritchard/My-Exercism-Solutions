using System;
using System.Collections.Generic;
using System.Linq;

public static class AtbashCipher
{
    public static readonly int min = (int)'a';
    public static readonly int max = (int)'z';
    const int chunkSize = 5;

    private static char Adjust(char source) =>
        char.IsLetter(source)
            ? (char)(((int)source - min) * -1 + max)
            : source;

    private static string Chunked(IEnumerable<char> source)
    {
        var chunks = Enumerable.Range(0, (source.Count() / chunkSize) + 1)
            .Select(i => string.Concat(source.Skip(i * chunkSize).Take(chunkSize)));
        return string.Join(" ", chunks).Trim();
    }

    public static string Encode(string plainValue) =>
        Chunked(plainValue
            .ToLower()
            .Where(char.IsLetterOrDigit)
            .Select(Adjust));

    public static string Decode(string encodedValue) =>
        Encode(encodedValue).Replace(" ", "");
}
