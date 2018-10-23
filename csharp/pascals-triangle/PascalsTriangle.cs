using System;
using System.Linq;
using System.Collections.Generic;

public static class PascalsTriangle
{
    public static IEnumerable<IEnumerable<int>> Calculate(int rows)
    {
        if(rows < 0)
            throw new ArgumentOutOfRangeException();

        var row = new int[0];
        var result = new List<int[]>();
        for(var current = 1; current <= rows; current++)
        {
            row = new[] { 1 }.Concat(
                    Enumerable.Range(0, current - 1).Select(i => 
                        (i >= row.Length ? 1 : row[i]) +
                        (i + 1 >= row.Length ? 0 : row[i + 1])))
                    .ToArray();
            result.Add(row);
        }
        return result.ToArray();
    }
}