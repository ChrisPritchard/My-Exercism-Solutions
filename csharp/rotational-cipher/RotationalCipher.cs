using System;
using System.Linq;

public static class RotationalCipher
{
    private static readonly (int, int) lower = ((int)'a', (int)'z');
    private static readonly (int, int) upper = ((int)'A', (int)'Z');

    public static string Rotate(string text, int shiftKey)
        => string.Concat(text.Select(o => char.IsLetter(o) ? Shift(o, shiftKey) : o));

    private static char Shift(char source, int shiftKey)
    {
        var (min, max) = char.IsLower(source) ? lower : upper;
        var newVal = (int)source + shiftKey;
        return (char)(newVal > max ? min + (newVal - max - 1) : newVal);
    }   
}