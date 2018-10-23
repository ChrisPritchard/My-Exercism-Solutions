using System;
using System.Linq;
using System.Collections.Generic;

public class NucleotideCount
{
    private readonly char[] valid = new[] { 'A', 'G', 'C', 'T' };

    public NucleotideCount(string sequence)
    {
        NucleotideCounts = sequence.ToCharArray().GroupBy(o => o).ToDictionary(o => o.Key, o => o.Count());
        foreach(var c in valid.Where(o => !NucleotideCounts.ContainsKey(o)))
            NucleotideCounts.Add(c, 0);
        if(!NucleotideCounts.Keys.All(o => valid.Contains(o)))
            throw new InvalidNucleotideException();
    }

    public IDictionary<char, int> NucleotideCounts { get; }
}

public class InvalidNucleotideException : Exception { }