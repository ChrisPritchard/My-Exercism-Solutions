using System;

public static class Bob
{
    public static string Response(string statement)
    {
        statement = statement.Trim();
        if (string.IsNullOrWhiteSpace(statement))
            return "Fine. Be that way!";
        else if (statement.ToUpper() == statement && statement.ToLower() != statement && statement.EndsWith('?'))
            return "Calm down, I know what I'm doing!";
        else if (statement.EndsWith('?'))
            return "Sure.";
        else if (statement.ToUpper() == statement && statement.ToLower() != statement)
            return "Whoa, chill out!";
        else
            return "Whatever.";
    }
}