using System;
using System.Linq;

public static class Triangle
{
    private static bool IsTriangle(double side1, double side2, double side3)
    {
        var sides = new[] { side1, side2, side3 }.OrderBy(o => o).ToArray();
        return sides.All(o => o > 0) && sides.Take(2).Sum() >= sides.Last();
    }

    public static bool IsScalene(double side1, double side2, double side3) 
        => IsTriangle(side1, side2, side3) && new[] { side1, side2, side3 }.Distinct().Count() == 3;

    public static bool IsIsosceles(double side1, double side2, double side3) 
        => IsTriangle(side1, side2, side3) && new[] { side1, side2, side3 }.Distinct().Count() <= 2;

    public static bool IsEquilateral(double side1, double side2, double side3) 
        => IsTriangle(side1, side2, side3) && new[] { side1, side2, side3 }.Distinct().Count() == 1;
}