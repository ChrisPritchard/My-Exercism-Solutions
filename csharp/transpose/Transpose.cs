using System;
using System.Linq;
using System.Collections.Generic;

public static class Transpose
{
    public static string String(string input)
    {
        var grid = input.Split('\n');
        var colLength = 0;
        for(var i = grid.Length - 1; i >= 0; i--)
        {
            grid[i] = grid[i].PadRight(colLength);
            colLength = grid[i].Length;
        }
        
        var output = new List<char>[colLength];
        
        for(var x = 0; x < grid.Length; x++)
            for(var y = 0; y < colLength; y++)
            {
                if(grid[x].Length <= y) continue;
                (output[y] ?? (output[y] = new List<char>()))
                    .Add(grid[x][y]);
            }

        var lines = output.Select(o => new string(o.ToArray()));
        return string.Join('\n', lines);
    }
}