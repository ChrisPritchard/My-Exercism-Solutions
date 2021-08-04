using System;

public static class ResistorColorTrio
{
    private static readonly string[] colours = new[] {
        "black", "brown", "red", "orange", "yellow", "green", "blue", "violet", "grey", "white" };

    private static int Value(string[] colors) => 
        (Array.IndexOf(colours, colors[0]) * 10 + Array.IndexOf(colours, colors[1]));

    public static string Label(string[] colors)
    {
        var val = Value(colors) * Math.Pow(10, Array.IndexOf(colours, colors[2]));
        return $"{val} ohms".Replace("000 ohms", " kiloohms");
    }
}
