using System;
using System.Collections.Generic;
using System.Linq;

public static class BracketPush
{
    private static readonly (char open, char close)[] pairs = new[]
    { ('[', ']'), ('{', '}'), ('(', ')') };

    public static bool IsPaired(string input)
    {
        var stack = new Stack<char>();
        foreach(var c in input)
            if(pairs.Any(p => p.open == c))
                stack.Push(c);
            else if(pairs.Any(p => p.close == c && stack.Count != 0 && stack.Peek() == p.open))
                stack.Pop();
            else if (pairs.Any(p => p.close == c))
                return false;
        return stack.Count == 0;
    }
}
