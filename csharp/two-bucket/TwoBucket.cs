using System;
using System.Collections.Generic;
using System.Linq;
using static System.Math;

public enum Bucket { One, Two }

public class TwoBucketResult
{
    public int Moves { get; set; }
    public Bucket GoalBucket { get; set; }
    public int OtherBucket { get; set; }
}

public class TwoBucket
{
    private readonly (int one, int two) max;
    private readonly (int, int) start;
    private readonly (int, int) invertStart;

    public TwoBucket(int bucketOne, int bucketTwo, Bucket startBucket)
        => (max, start, invertStart) = ((bucketOne, bucketTwo), 
            startBucket == Bucket.One ? (bucketOne, 0) : (0, bucketTwo),
            startBucket == Bucket.One ? (0, bucketTwo) : (bucketOne, 0));

    public TwoBucketResult Measure(int goal)
    {
        var startSet = new [] { invertStart, start };

        var result = FindMoves(startSet, goal).OrderBy(o => o.Count()).First();
        var last = result.Last();

        return new TwoBucketResult
        {
            Moves = result.Count() - 1,
            GoalBucket = last.one == goal ? Bucket.One : Bucket.Two,
            OtherBucket = last.one == goal ? last.two : last.one
        };
    }

    private IEnumerable<IEnumerable<(int one, int two)>> FindMoves(IEnumerable<(int one, int two)> soFar, int goal)
    {
        var last = soFar.Last();
        if(last.one == goal || last.two == goal)
        {
            yield return soFar;
            yield break;
        }

        var candidates = Candidates(last).Where(o => !soFar.Contains(o));
        foreach(var withCandidate in candidates.SelectMany(c => FindMoves(soFar.Append(c), goal)))
            yield return withCandidate;
    }

    private IEnumerable<(int, int)> Candidates((int one, int two) last)
    {
        yield return (last.one, 0);
        yield return (0, last.two);
        yield return (max.one, last.two);
        yield return (last.one, max.two);
        
        var oneWithTwo = Min(max.one, last.one + last.two);
        yield return (oneWithTwo, last.two - (oneWithTwo - last.one));

        var twoWithOne = Min(max.two, last.one + last.two);
        yield return (last.one - (twoWithOne - last.two), twoWithOne);
    }
}
