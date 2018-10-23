using System;
using System.Collections.Generic;

public class Robot
{
    private static readonly Random random = new Random();
    private static readonly List<string> used = new List<string>();
    private static readonly string alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    private static string GetName ()
    {
        while(true)
        {
            var candidate = $"{alpha[random.Next(25)]}{alpha[random.Next(25)]}{random.Next(9)}{random.Next(9)}{random.Next(9)}";
            if(!used.Contains(candidate))
            {
                used.Add(candidate);
                return candidate;
            }
        }
    }

    public string Name { get; private set; } = GetName();

    public void Reset() => Name = GetName();
}