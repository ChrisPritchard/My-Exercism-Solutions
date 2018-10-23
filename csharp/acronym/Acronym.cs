using System;
using System.Linq;

public static class Acronym
{
    public static string Abbreviate(string phrase) => 
        string.Concat(phrase.Split(' ', '-').Select(o => char.ToUpper(o[0])));
}