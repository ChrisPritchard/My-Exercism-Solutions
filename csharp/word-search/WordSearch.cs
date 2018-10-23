using System;
using System.Collections.Generic;
using System.Linq;

public class WordSearch
{
    private readonly string[] grid;

    public WordSearch(string grid) 
        => this.grid = grid.Split('\n');

    public Dictionary<string, ((int, int), (int, int))?> Search(string[] wordsToSearchFor)
        => wordsToSearchFor.ToDictionary(word => word, word => FindIndex(word));

    private readonly (int, int)[] directions = new []
    { (-1,-1), (0,-1), (1,-1), (1,0), (1,1), (0,1), (-1,1), (-1,0) };

    private ((int, int), (int, int))? FindIndex(string word)
        => grid
            .SelectMany((line, y) => 
                line.Select((c, x) => (c, x, y)))
            .Where(point => point.c == word[0])
            .SelectMany(point => 
                directions.Select(direction => Crawl(word, point.x, point.y, direction)))
            .Where(o => o != null)
            .FirstOrDefault();

    private ((int, int), (int, int))? Crawl(string word, int x, int y, (int dx, int dy) direction)
    {
        var coords = word
            .Select((c, i) => 
                (c, nx: x + i * direction.dx, ny: y + i * direction.dy))
            .ToArray();
        var valid = coords.All(coord => 
            {
                try { return grid[coord.ny][coord.nx] == coord.c; }
                catch { return false; }
            });

        if(!valid)
            return null;
            
        var end = coords.Last();
        return ((x + 1, y + 1), (end.nx + 1, end.ny + 1));
    }
}