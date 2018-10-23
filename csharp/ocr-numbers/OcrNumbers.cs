using System;
using System.Collections.Generic;
using System.Linq;

public static class OcrNumbers
{
    private static IEnumerable<IEnumerable<T>> ChunkBySize<T>(int size, IEnumerable<T> source) =>
        Enumerable.Range(0, source.Count() / size).Select(i => source.Skip(i * size).Take(size));

    private static IEnumerable<IEnumerable<T>> Transpose<T>(IEnumerable<IEnumerable<T>> source) =>
        source.SelectMany(o => o.Select((s, i) => (item:s, index:i)))
            .GroupBy(s => s.index).Select(s => s.Select(t => t.item));

    private static readonly (string[] tokens, char number)[] numbers =
        ChunkBySize(3,
        " _     _  _     _  _  _  _  _ " +
        "| |  | _| _||_||_ |_   ||_||_|" +
        "|_|  ||_  _|  | _||_|  ||_| _|" +
        "                              ")
        .Select((s, i) => (text:new string(s.ToArray()), index:i))
        .GroupBy(o => o.index % 10)
        .Select(o => (o.Select(c => c.text).ToArray(), o.Key.ToString()[0]))
        .ToArray();
    
    private static char OcrNumber(IEnumerable<string> tokens) =>
        numbers
            .Where(o => tokens.Zip(o.tokens, (o1, o2) => o1 == o2).All(r => r))
            .DefaultIfEmpty((new string[0], number:'?'))
            .Single().number;

    public static string Convert(string input)
    {
        var lines = input.Split('\n');
        if(lines.Length % 4 != 0 || lines.Any(line => line.Length % 3 != 0))
            throw new ArgumentException();
        
        var ocrRows = 
            ChunkBySize(4, lines)
            .Select(line => 
            {
                var rows = line.Select(row => 
                    ChunkBySize(3, row).Select(chars => new string(chars.ToArray())));
                var ocrTokens = Transpose(rows);
                return string.Concat(ocrTokens.Select(OcrNumber));
            });
        return string.Join(",", ocrRows);
    }
}