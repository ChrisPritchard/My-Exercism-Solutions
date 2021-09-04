using System;

public static class Identifier
{
    public static string Clean(string identifier)
    {
        var result = "";
        var upperNext = false;
        foreach(var c in identifier) {
            if (c == ' ')
                result += "_";
            else if(c < 13)
                result += "CTRL";
            else if (c >= 'α' && c <= 'ω')
                continue;
            else if (c == '-')
                upperNext = true;
            else if (upperNext)
            {
                result += char.ToUpper(c);
                upperNext = false;
            }
            else if (char.IsLetter(c))
                result += c;
        }
        return result;
    }
}
