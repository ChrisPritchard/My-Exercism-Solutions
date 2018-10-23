using System;
using System.Linq;

public enum Classification
{
    Perfect,
    Abundant,
    Deficient
}

public static class PerfectNumbers
{
    public static Classification Classify(int number)
    {
        var aliquot = Enumerable.Range(1, number - 1).Where(o => number / o * o == number).Sum();
        return aliquot == number 
            ? Classification.Perfect 
            : aliquot > number 
                ? Classification.Abundant 
                : Classification.Deficient;
    }
}
