using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

public static class Tournament
{   
    public static void Tally(Stream inStream, Stream outStream)
    {
        string line(string team, int matchesPlayed, int wins, int losses, int draws, int points)
            => $"\n{team, -31}| {matchesPlayed, 2} | {wins, 2} | {losses, 2} | {draws, 2} | {points, 2}";
        
        using(var writer = new StreamWriter(outStream))
        {
            writer.Write($"{"Team", -31}| MP |  W |  D |  L |  P");
            
            var teams = ProcessInStream(inStream);

            teams
                .Select(o => 
                    new 
                    {
                        team = o.Key, 
                        matchesPlayed = o.Value.wins + o.Value.losses + o.Value.draws, 
                        wins = o.Value.wins, 
                        draws = o.Value.draws, 
                        losses = o.Value.losses,
                        points = o.Value.wins * 3 + o.Value.draws
                    })
                .OrderByDescending(o => o.points).ThenBy(o => o.team)
                .ToList()
                .ForEach(o => 
                    writer.Write(line(o.team, o.matchesPlayed, o.wins, o.draws, o.losses, o.points)));
        }
    }

    private static Dictionary<string, (int wins, int losses, int draws)> ProcessInStream(Stream inStream)
    {
        var teams = new Dictionary<string, (int wins, int losses, int draws)>();
        
        void setResult(string team, int wins, int losses, int draws)
        {
            if(teams.ContainsKey(team))
            {
                var existing = teams[team];
                wins += existing.wins;
                losses += existing.losses;
                draws += existing.draws;
            }
            
            teams[team] = (wins, losses, draws);
        }

        using(var reader = new StreamReader(inStream))
        {
            string text;
            while((text = reader.ReadLine()) != null)
            {
                var segments = text.Split(';');
                if(segments[2] == "win")
                {
                    setResult(segments[0],1,0,0);
                    setResult(segments[1],0,1,0);
                }
                else if(segments[2] == "loss")
                {
                    setResult(segments[0],0,1,0);
                    setResult(segments[1],1,0,0);
                }
                else
                {
                    setResult(segments[0],0,0,1);
                    setResult(segments[1],0,0,1);
                }
            }
        }

        return teams;
    }
}