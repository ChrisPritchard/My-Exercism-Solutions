using System;

public static class ResistorColorDuo
{
    private static readonly string[] colours = new[] {
        "black", "brown", "red", "orange", "yellow", "green", "blue", "violet", "grey", "white" };

    public static int Value(string[] colors) => 
        Array.IndexOf(colours, colors[0]) * 10 + Array.IndexOf(colours, colors[1]);
}
