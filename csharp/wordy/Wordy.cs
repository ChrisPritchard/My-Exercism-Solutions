using System;

public static class Wordy
{
    const string start = "What is ";

    public static int Answer(string question)
    {
        if(!question.StartsWith(start))
            throw new ArgumentException();

        var elements = 
            question.Substring(start.Length)
            .Replace("multiplied by", "multipliedby")
            .Replace("divided by", "dividedby")
            .TrimEnd('?')
            .Split(' ');

        if (elements.Length % 2 != 1)
            throw new ArgumentException();

        int parse(string source) => 
            int.TryParse(source, out int parsed) 
            ? parsed : throw new ArgumentException();

        var result = parse(elements[0]);

        for(var i = 1; i < elements.Length; i += 2)
        {
            var op = elements[i];
            var num = parse(elements[i + 1]);
            result = ActOut(op, result, num);
        }

        return result;
    }

    private static int ActOut(string op, int result, int num)
    {
        switch(op)
        {
            case "plus": return result + num;
            case "minus": return result - num;
            case "multipliedby": return result * num;
            case "dividedby": return result / num;
            default: throw new ArgumentException();
        }
    }
}