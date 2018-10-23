using System;
using System.Collections.Generic;
using System.Linq;

public static class ScaleGenerator
{
    private static readonly string[] allPitches = new [] { "A", "A#", "B", "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#" };
    private static readonly string[] flatPitches = new [] { "A", "Bb", "B", "C", "Db", "D", "Eb", "E", "F", "Gb", "G", "Ab" };
    private static readonly string[] useFlats = new [] { "F", "Bb", "Eb", "Ab", "Db", "Gb", "d", "g", "c", "f", "bb", "eb" };

    public static string[] Pitches(string tonic, string pattern = "mmmmmmmmmmmm")
    {
        var pitchList = useFlats.Contains(tonic) ? flatPitches.ToList() : allPitches.ToList();
        tonic = tonic.Length == 1 ? tonic.ToUpper() : tonic[0].ToString().ToUpper() + tonic.Substring(1);

        IEnumerable<string> Next(string current, IEnumerable<char> remaining)
        {
            var (head, tail) = (remaining.First(), remaining.Skip(1));

            var nextIndex = pitchList.IndexOf(current) + (head == 'M' ? 2 : (head == 'A' ? 3 : 1));
            nextIndex = nextIndex >= pitchList.Count ? nextIndex - pitchList.Count : nextIndex;

            var newPitch = pitchList[nextIndex];
            if(newPitch == tonic)
                yield break;
            
            yield return newPitch;

            foreach(var item in Next(newPitch, tail))
                yield return item;
        }

        return new [] { tonic }.Concat(Next(tonic, pattern.ToList())).ToArray();
    }
}