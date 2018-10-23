using System;
using System.Collections.Generic;
using System.Linq;

public static class RomanNumeralExtension
{
    private static readonly (string one, string five, string ten)[] numerals = new[]
    {
        ("M", "", ""),
        ("C", "D", "M"),
        ("X", "L", "C"),
        ("I", "V", "X")
    };

    private static string[] ForNumber(string one, string five, string ten) => new[]
    {
        "",
        one,
        $"{one}{one}",
        $"{one}{one}{one}",
        $"{one}{five}",
        five,
        $"{five}{one}",
        $"{five}{one}{one}",
        $"{five}{one}{one}{one}",
        $"{one}{ten}"
    };

    private static string[] ForNumber((string one, string five, string ten) numerals) =>
        ForNumber(numerals.one, numerals.five, numerals.ten);

    public static string ToRoman(this int value)
    {
        var converted = value.ToString().PadLeft(4, '0')
            .Select(n => int.Parse(n.ToString()))
            .Select((n, i) => ForNumber(numerals[i])[n]);
        return string.Concat(converted);
    }
}