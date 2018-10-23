using System;
using System.Collections.Generic;
using System.Linq;

public class SaddlePoints
{
    private readonly int[,] values;

    public SaddlePoints(int[,] values) => this.values = values;

    public IEnumerable<(int, int)> Calculate()
    {        
        var (rows, cols) = (values.GetLength(0), values.GetLength(1));
        var maxPerRow = Enumerable.Range(0, rows).Select(r => Enumerable.Range(0, cols).Select(c => values[r, c]).Max()).ToArray();
        var minPerCol = Enumerable.Range(0, cols).Select(c => Enumerable.Range(0, rows).Select(r => values[r, c]).Min()).ToArray();

        return Enumerable.Range(0, rows)
            .SelectMany(r => Enumerable.Range(0, cols)
                .Where(c => values[r, c] == maxPerRow[r] && values[r, c] == minPerCol[c])
                .Select(c => (r, c)))
                .ToArray();
    }
}