using System;
using System.Diagnostics;
using System.Linq;
using static System.Math;

public static class RealNumberExtension
{
    public static double Expreal(this int realNumber, RationalNumber r) => r.Expreal(realNumber);
}

public struct RationalNumber
{
    private int a;
    private int b;

    public RationalNumber(int numerator, int denominator)
    {
        a = denominator < 0 ? numerator * -1 : numerator;
        b = denominator < 0 ? denominator * -1 : denominator;
        if(a == 0)
            b = 1;
    }

    public RationalNumber Add(RationalNumber r)
        => new RationalNumber(a * r.b + r.a * b, b * r.b).Reduce();

    public static RationalNumber operator +(RationalNumber r1, RationalNumber r2)
        => r1.Add(r2);

    public RationalNumber Sub(RationalNumber r)
        => new RationalNumber(a * r.b - r.a * b, b * r.b).Reduce();

    public static RationalNumber operator -(RationalNumber r1, RationalNumber r2)
        => r1.Sub(r2);

    public RationalNumber Mul(RationalNumber r)
        => new RationalNumber(a * r.a, b * r.b).Reduce();

    public static RationalNumber operator *(RationalNumber r1, RationalNumber r2)
        => r1.Mul(r2);

    public RationalNumber Div(RationalNumber r)
        => new RationalNumber(a * r.b, r.a * b).Reduce();

    public static RationalNumber operator /(RationalNumber r1, RationalNumber r2)
        => r1.Div(r2);

    public RationalNumber Abs()
        => new RationalNumber(Math.Abs(a), Math.Abs(b));

    public RationalNumber Reduce()
    {
        var (n, d) = (Math.Abs(a), Math.Abs(b));
        var reducer = Enumerable.Range(2, d-1).FirstOrDefault(factor => 
                   n / factor > 0  && d / factor > 0 
                && n % factor == 0 && d % factor == 0);
        return reducer != 0 ? new RationalNumber(a / reducer, b / reducer) : this;
    }

    public RationalNumber Exprational(int power)
        => power > 0 
            ? new RationalNumber((int)Pow(a, power), (int)Pow(b, power))
            : new RationalNumber((int)Pow(b, Math.Abs(power)), (int)Pow(a, Math.Abs(power)));

    public double Expreal(int baseNumber)
        => Pow(Pow(baseNumber, a), 1.0/(double)b);
}