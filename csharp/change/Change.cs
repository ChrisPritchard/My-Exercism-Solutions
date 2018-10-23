using System;
using System.Collections.Generic;
using System.Linq;

public static class Change
{
    public static int[] FindFewestCoins(int[] coins, int target)
    {
        IEnumerable<int> test(int sum, IEnumerable<int> remaining, IEnumerable<int> result, int maxLength)
        {
            if(result.Count() == maxLength || !remaining.Any()) 
                return result;
            
            var head = remaining.First();
            var tail = remaining.Skip(1);
            if(sum < head)
                return test(sum, tail, result, maxLength);
            var subresult = test(sum - head, remaining, result.Append(head), maxLength);
            if (subresult.Sum() != target)
                return test(sum, tail, result, maxLength);
            var otherresult = test(sum, tail, result, subresult.Count() - 1);
            return otherresult.Sum() == target ? otherresult : subresult;
        }

        var ordered = coins.OrderByDescending(o => o);
        var finalResult = test(target, ordered, Enumerable.Empty<int>(), target).OrderBy(o => o);
        if(finalResult.Sum() != target)
            throw new ArgumentException();
        return finalResult.ToArray();
    }
}