using System;
using System.Collections.Generic;
using System.Linq;

public static class RnaTranscription
{
    private static readonly Dictionary<char, char> map = new Dictionary<char, char>
    {
        {'G', 'C'}, {'C', 'G'}, {'T', 'A'}, {'A', 'U'}
    };

    public static string ToRna(string nucleotide)
        => string.Concat(nucleotide.Select(c => map[c]));
}