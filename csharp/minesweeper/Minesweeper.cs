using System;
using System.Linq;

public static class Minesweeper
{
    public static string[] Annotate(string[] input)
    {
        var mineCounts =
            input.SelectMany((line, y) => 
                line
                .Select((c, x) => (isMine: c == '*', x, y))
                .Where(cell => cell.isMine))
            .SelectMany(cell => 
                Enumerable.Range(cell.x - 1, 3).SelectMany(x => 
                Enumerable.Range(cell.y - 1, 3).Where(y =>
                    x != cell.x || y != cell.y)
                .Select(y => (x, y))))
            .GroupBy(cell => cell)
            .ToDictionary(cell => (cell.Key.x, cell.Key.y), cell => cell.Count());
        
        return input.Select((line, y) => 
            string.Concat(line.Select((c, x) => 
                c == '*' ? "*"
                : (mineCounts.ContainsKey((x, y)) 
                    ? mineCounts[(x, y)].ToString() 
                    : " "))))
            .ToArray();
    }
}
