using System;
using System.Collections.Generic;
using System.Linq;

public static class ProteinTranslation
{
    private static (string[],string)[] codonMap = new[]
    {
        (new[] {"AUG"}, "Methionine"),
        (new[] {"UUU", "UUC"}, "Phenylalanine"),
        (new[] {"UUA", "UUG"}, "Leucine"),
        (new[] {"UCU", "UCC", "UCA", "UCG"}, "Serine"),
        (new[] {"UAU", "UAC"}, "Tyrosine"),
        (new[] {"UGU", "UGC"}, "Cysteine"),
        (new[] {"UGG"}, "Tryptophan"),
        (new[] {"UAA", "UAG", "UGA"}, null)
    };

    public static string[] Proteins(string strand)
        => Enumerable.Range(0, strand.Length / 3)
            .Select(o => strand.Substring(o * 3, 3))
            .Aggregate((Enumerable.Empty<string>(), false), (o, c) => 
            {
                var (set, done) = o;
                if (done) return o;

                var amino = codonMap.Single(map => map.Item1.Contains(c)).Item2;
                return amino == null ? (set, true) : (set.Append(amino), false);
            })
            .Item1.ToArray();
}