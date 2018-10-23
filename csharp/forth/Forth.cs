using System;
using System.Linq;
using System.Collections.Generic;
using Sprache;

public static class Forth
{
    public static string Evaluate(string[] instructions)
    {
        var customOps = new Dictionary<string, IEnumerable<string>>();
        foreach(var definition in instructions.Take(instructions.Length - 1).Select(CustomOp.Parse))
            customOps[definition.token.ToLower()] = definition.replacements.Reverse().SelectMany(o => 
                customOps.ContainsKey(o) ? customOps[o] : new [] { o.ToLower() }).ToArray();

        var stack = new Stack<string>(instructions.Last().ToLower().Split(' ').Reverse());
        var memory = new Stack<int>();

        while(stack.Count > 0)
        {
            var next = stack.Pop();
            if(customOps.ContainsKey(next))
                customOps[next].ToList().ForEach(stack.Push);
            else if(standardOps.ContainsKey(next))
                standardOps[next](memory);
            else if(Parse.Number.TryParse(next).WasSuccessful)
                memory.Push(int.Parse(next));
            else
                throw new InvalidOperationException();
        }

        return string.Join(' ', memory.Reverse());
    }

    private static readonly Dictionary<string, Action<Stack<int>>> standardOps = new Dictionary<string, Action<Stack<int>>>
    {
        ["+"] = memory => memory.Push(memory.Pop() + memory.Pop()),
        ["-"] = memory => memory.Push(-memory.Pop() + memory.Pop()),
        ["*"] = memory => memory.Push(memory.Pop() * memory.Pop()),
        ["/"] = memory => 
        {
            var divisor = memory.Pop();
            if(divisor == 0) throw new InvalidOperationException();
            memory.Push(memory.Pop() / divisor);
        },
        ["dup"] = memory => 
        {
            var toDup = memory.Pop();
            memory.Push(toDup);
            memory.Push(toDup);
        },
        ["swap"] = memory => 
        {
            var one = memory.Pop();
            var two = memory.Pop();
            memory.Push(one);
            memory.Push(two);
        },
        ["over"] = memory => 
        {
            var one = memory.Pop();
            var two = memory.Pop();
            memory.Push(two);
            memory.Push(one);
            memory.Push(two);
        },
        ["drop"] = memory => memory.Pop()
    };

    private static readonly Parser<(string token, IEnumerable<string> replacements)> CustomOp =
        from _ in Parse.String(": ")
        from token in Parse.AnyChar.Except(Parse.Number).Until(Parse.Char(' ')).Text()
        from replacements in Parse.Many(Parse.AnyChar.Until(Parse.Char(' ')).Text())
        from __ in Parse.Char(';')
        select (token, replacements);
}