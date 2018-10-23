using System;
using System.Linq;
using System.Collections.Generic;
using static System.Math;

public static class CryptoSquare
{
    public static string Ciphertext(string plaintext)
    {
        var clean = new string(plaintext.ToLower().Where(char.IsLetterOrDigit).ToArray());
        if(clean.Length == 0)
            return "";

        var sqrt = Sqrt((float)clean.Length);
        var (cols, rows) = ((int)Floor(sqrt), (int)Ceiling(sqrt));
        if(cols * rows < clean.Length) 
            cols++;

        var square = Enumerable.Range(0, cols).Select(i => 
            new string(clean.Skip(i * rows).Take(rows).ToArray()).PadRight(rows))
                .ToArray();

        var cipher = Enumerable.Range(0, rows).Select(c => 
            new string(Enumerable.Range(0, cols)
                .Select(r => square[r][c]).ToArray()));

        return string.Join(' ', cipher);
    }
}