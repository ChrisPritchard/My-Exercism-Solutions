using System;
using System.Linq;

public static class RunLengthEncoding
{
    private class CounterState
    {
        public int Count { get;set; }
        public char? Counted { get;set; }
        public string Result { get;set; } = "";

        public string AsResult() => $"{Result}{(Count == 1 ? "" : Count.ToString())}{Counted}";
    }

    public static string Encode(string input)
    {
        CounterState counter(CounterState previous, char current)
        {
            if(previous.Counted == current)
                return new CounterState 
                { 
                    Count = previous.Count + 1, 
                    Counted = current, 
                    Result = previous.Result 
                };
            var newResult = previous.Counted == null ? "" : previous.AsResult();
            return new CounterState { Count = 1, Counted = current, Result = newResult };
        }

        var processed = input.Aggregate<char, CounterState>(new CounterState(), counter);
        return processed.Count == 0 ? processed.Result : processed.AsResult();
    }

    public static string Decode(string input)
    {
        CounterState printer(CounterState previous, char current)
        {
            if(!char.IsDigit(current))
            {
                var newLength = previous.Result.Length + 
                    (previous.Count == 0 ? 1 : previous.Count);
                return new CounterState 
                { 
                    Count = 0, 
                    Result = previous.Result
                        .PadRight(newLength, current) 
                };
            }
            var number = int.Parse(current.ToString());
            return new CounterState { Count = previous.Count * 10 + number, Result = previous.Result };
        }

        var processed = input.Aggregate<char, CounterState>(new CounterState(), printer);
        return processed.Result;
    }
}
