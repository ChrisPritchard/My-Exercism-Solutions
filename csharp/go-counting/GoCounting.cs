using System;
using System.Collections.Generic;
using System.Linq;

public enum Owner
{
    None, Black, White
}

public class GoCounting
{
    private string[] board;

    public GoCounting(string input) => board = input.Split('\n');

    public (Owner owner, IEnumerable<(int, int)> tiles) Territory((int x, int y) coord)
    {
        if(coord.x < 0 || coord.y < 0 || coord.y >= board.Length || coord.x >= board[0].Length)
            throw new ArgumentException();

        var start = OwnerFor(coord);
        if(start != Owner.None)
            return (Owner.None, Enumerable.Empty<(int, int)>());
        
        var extent = Extent(coord.x, coord.y);
        var owners = extent
            .Select(o => o.owner)
            .Where(o => o != Owner.None)
            .Distinct();
        var areEmpty = extent
            .Where(o => o.owner == Owner.None)
            .Select(o => (x: o.x, y: o.y))
            .OrderBy(o => o.x).ThenBy(o => o.y);
        return (owners.Count() == 1 ? owners.First() : Owner.None, areEmpty);
    }

    private IEnumerable<(int x, int y, Owner owner)> Extent(int x, int y)
    {
        var soFar = new Dictionary<(int x, int y), Owner> { { (x, y), Owner.None } };
        var neighbours = Adjacent(x, y);
        while(neighbours.Any())
        {
            var withOwners = neighbours.Select(o => (x: o.x, y: o.y, owner: OwnerFor(o))).ToList();
            withOwners.ForEach(o => soFar.Add((o.x, o.y), o.owner));

            neighbours = withOwners
                .Where(o => o.owner == Owner.None)
                .SelectMany(o => Adjacent(o.x, o.y))
                .Where(o => !soFar.ContainsKey((o.x, o.y)))
                .ToArray();
        }

        return soFar.Select(o => (o.Key.x, o.Key.y, o.Value));
    }

    private IEnumerable<(int x, int y)> Adjacent(int x, int y)
        => new (int dx, int dy)[] { (-1, 0), (1, 0), (0, -1), (0, 1) }
            .Select(o => (x: x + o.dx, y: y + o.dy))
            .Where(o => o.x >= 0 && o.y >= 0 && o.y < board.Length && o.x < board[0].Length);

    private Owner OwnerFor(char c) => c == ' ' ? Owner.None : c == 'W' ? Owner.White : Owner.Black;

    private Owner OwnerFor((int x, int y) coord) => OwnerFor(board[coord.y][coord.x]);

    public Dictionary<Owner, IEnumerable<(int, int)>> Territories() 
    {
        var result = new Dictionary<Owner, IEnumerable<(int, int)>>
        {
            [Owner.Black] = Array.Empty<(int, int)>(),
            [Owner.White] = Array.Empty<(int, int)>(),
            [Owner.None] = Array.Empty<(int, int)>()
        };

        var allTiles = 
            Enumerable.Range(0, board.Length).SelectMany(y =>
            Enumerable.Range(0, board[0].Length).Select(x => Territory((x, y))));
        var hasTerritory = 
            allTiles.GroupBy(o => o.owner)
            .ToDictionary(o => o.Key, 
                o => o.SelectMany(t => t.tiles).Distinct());

        foreach(var owner in hasTerritory.Keys)
            result[owner] = hasTerritory[owner];

        return result;
    }
}