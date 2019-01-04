using System;
using System.Collections.Generic;
using System.Linq;

public class HighScores
{
    private readonly List<int> list;

    public HighScores(List<int> list) => this.list = list;

    public List<int> Scores() => list;

    public int Latest() => list.LastOrDefault();

    public int PersonalBest() => list.Max();

    public List<int> PersonalTop() => list.OrderByDescending(o => o).Take(3).ToList();

    public string Report()
    {
        var latest = Latest();
        var best = PersonalBest();
        return latest == best 
            ? $"Your latest score was {best}. That's your personal best!"
            : $"Your latest score was {latest}. That's {best - latest} short of your personal best!";
    }
}