using System;
using System.Linq;
using System.Collections.Generic;

public class BowlingGame
{
    private List<int> currentFrame;
    private readonly List<int[]> frames = new List<int[]>();

    private bool IsFinished() => frames.Count >= 10 && (frames.Count != 10 || frames[9].Sum() != 10);

    public void Roll(int pins) 
    {
        if(pins < 0 || pins > 10 || IsFinished())
            throw new ArgumentException();

        if(currentFrame == null)
            currentFrame = new List<int> { pins };
        else
            currentFrame.Add(pins);
        
        var frameSum = currentFrame.Sum();
        if(frameSum > 10 && (frames.Count != 10 || currentFrame[0] != 10))
            throw new ArgumentException();
        
        if((frameSum == 10 && frames.Count != 10) 
        || currentFrame.Count == 2 
        || (frames.Count == 10 && frames[9].Length != 1))
        {
            frames.Add(currentFrame.ToArray());
            currentFrame = null;
        }
    }

    public int? Score()
    {
        if(!IsFinished())
            throw new ArgumentException();

        return Enumerable.Range(0, 10).Select(i => 
        {
            var frame = frames[i];
            var score = frame.Sum();
            if(score == 10 && frame.Length == 2)
                return score + frames[i+1][0];
            else if(score == 10 && frame.Length == 1)
                return 
                    frames[i+1].Length == 1 
                    ? score + frames[i+1][0] + frames[i+2][0]
                    : score + frames[i+1].Sum();
            else return score;
        }).Sum();
    }
}