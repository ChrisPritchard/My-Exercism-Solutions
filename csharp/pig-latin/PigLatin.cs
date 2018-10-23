using System;
using System.Linq;

public static class PigLatin
{
    private static readonly string[] vowels = new [] 
        { "a", "e", "i", "o", "u", "xr", "yt" };
    private const string special = "qu";

    public static string Translate(string word)
    {
        if(word.IndexOf(' ') > -1)
            return string.Join(' ', word.Split(' ').Select(Translate));

        var startingVowel = vowels.FirstOrDefault(word.StartsWith);
        if(startingVowel != null)
            return word + "ay";

        var specialIndex = word.IndexOf(special);
        if(specialIndex != -1)
            return 
                word.Substring(specialIndex + special.Length)
                + word.Substring(0, specialIndex)
                + special + "ay";

        var firstVowel = vowels
            .Select(v => word.IndexOf(v))
            .Where(i => i != -1)
            .DefaultIfEmpty(0)
            .Min();
        var consonantSound = firstVowel == 0 
            ? word : word.Substring(0, firstVowel);
        consonantSound = consonantSound.Length > 1 
            ? consonantSound.TrimEnd('y') : consonantSound;

        return $"{word.Substring(consonantSound.Length)}{consonantSound}ay";
    }
}