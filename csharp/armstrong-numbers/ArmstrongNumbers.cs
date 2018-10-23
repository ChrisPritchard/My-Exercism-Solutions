using System;
using System.Linq;

public static class ArmstrongNumbers
{
    public static bool IsArmstrongNumber(int number)
    {
        var nums = number.ToString().Select(c => int.Parse(c.ToString())).ToArray();
        return nums.Select(n => Math.Pow(n, nums.Length)).Sum() == number;
    }
}