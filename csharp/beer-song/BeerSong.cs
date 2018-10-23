using System;
using System.Text;

public static class BeerSong
{
    public static string Recite(int startBottles, int takeDown)
    {
        var recited = new StringBuilder();
        
        string plural(int count) => count == 1 ? "" : "s";
        string number(int count, bool capitalise = false) => count == 0 ? $"{(capitalise ? "N" : "n")}o more" : count.ToString();
        string onTheWall(int count, bool capitalise = false) => $"{number(count, capitalise)} bottle{plural(count)} of beer on the wall";
        string ofBeer(int count) => $"{number(count)} bottle{plural(count)} of beer.";

        while(takeDown > 0)
        {
            recited.Append($"{onTheWall(startBottles, true)}, {ofBeer(startBottles)}\n");

            startBottles -= 1;
            takeDown -= 1;

            if(startBottles >= 0)
                recited.Append($"Take {(startBottles > 0 ? "one" : "it")} down and pass it around, {onTheWall(startBottles)}.");
            else
                recited.Append($"Go to the store and buy some more, {onTheWall(99)}.");

            if(takeDown > 0)
                recited.Append("\n\n");
        }

        return recited.ToString();
    }
}