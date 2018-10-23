using System;
using System.Collections.Generic;
using System.Linq;

public enum ConnectWinner { White, Black, None }

public class Connect
{
    private readonly string[] grid;

    public Connect(string[] input) => 
        grid = input.Select(o => o.Replace(" ", "")).ToArray();
    
    public ConnectWinner Result()
    {
        var startSets = Enumerable.Empty<(int, int)>();
        bool blackWin(int _, int col) => col == grid[0].Length - 1;
        bool whiteWin(int row, int _) => row == grid.Length - 1;

        if(Enumerable.Range(0, grid.Length).Where(r => 
            grid[r][0] == 'X').Any(r => 
                Crawl(startSets, r, 0, 'X', blackWin)))
            return ConnectWinner.Black;
            
        if(Enumerable.Range(0, grid[0].Length).Where(c => 
            grid[0][c] == 'O').Any(c => 
                Crawl(startSets, 0, c, 'O', whiteWin)))
            return ConnectWinner.White;

        return ConnectWinner.None;
    }

    private bool Crawl(IEnumerable<(int, int)> soFar, int row, int col, char target, Func<int, int, bool> hasWon)
    {
        if(row < 0 || row == grid.Length || col < 0 || col == grid[0].Length 
        || grid[row][col] != target || soFar.Contains((row, col)))
            return false;
        if(hasWon(row, col))
            return true;
        
        var newSoFar = soFar.Append((row, col));
        return new []
        {
            Crawl(newSoFar, row - 1, col, target, hasWon),
            Crawl(newSoFar, row + 1, col, target, hasWon),
            Crawl(newSoFar, row - 1, col + 1, target, hasWon),
            Crawl(newSoFar, row, col + 1, target, hasWon),
            Crawl(newSoFar, row + 1, col - 1, target, hasWon),
            Crawl(newSoFar, row, col - 1, target, hasWon)
        }.Any(o => o);
    }
}