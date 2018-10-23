using System;
using System.Collections.Generic;
using System.Linq;

public class RailFenceCipher
{
    private readonly int rails;

    public RailFenceCipher(int rails) => this.rails = rails;

    public string Encode(string input)
    {
        var rows = Fence(input.Length, i => input[i]);
        return string.Concat(rows.Select(row => new string(row)));
    }

    public string Decode(string input)
    {
        var rows = Fence(input.Length, i => i);
        var indexes = rows.SelectMany(o => o).ToArray();
        var output = new char[input.Length];
        for(var i = 0; i < indexes.Length; i++)
            output[indexes[i]] = input[i];
        return new string(output);
    }

    private IEnumerable<T[]> Fence<T>(int length, Func<int,T> subjectFinder)
    {
        var row = 0;
        var down = true;
        return
            Enumerable.Range(0, length).Select(i =>
            {
                var result = (row: row, subject: subjectFinder(i));
                row = down ? row + 1 : row - 1;
                if (row < 0) { down = true; row = 1; }
                if (row == rails) { down = false; row = rails - 2; }
                return result;
            })
            .GroupBy(o => o.row)
            .Select(o => o.Select(n => n.subject).ToArray());
    }
}