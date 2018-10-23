using System;
using System.Linq;
using System.Collections.Generic;

public static class Etl
{
    public static Dictionary<string, int> Transform(Dictionary<int, string[]> old)
        => old
            .SelectMany(o => o.Value.Select(character => (key: character.ToLower(), value: o.Key)))
            .Distinct().ToDictionary(o => o.key, o => o.value);
}