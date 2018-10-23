using System;

public struct ComplexNumber
{
    private readonly double a;
    private readonly double b;

    public ComplexNumber(double real, double imaginary) => (a, b) = (real, imaginary);

    private double Sqr(double source) => Math.Pow(source, 2);

    public double Real() => a;

    public double Imaginary() => b;

    public ComplexNumber Mul(ComplexNumber other)
    {
        var (c, d) = (other.a, other.b);
        return new ComplexNumber(a * c - b * d, b * c + a * d);
    }

    public ComplexNumber Add(ComplexNumber other)
    {
        var (c, d) = (other.a, other.b);
        return new ComplexNumber(a + c, b + d);
    }

    public ComplexNumber Sub(ComplexNumber other)
    {
        var (c, d) = (other.a, other.b);
        return new ComplexNumber(a - c, b - d);
    }

    public ComplexNumber Div(ComplexNumber other)
    {
        var (c, d) = (other.a, other.b);
        return new ComplexNumber((a * c + b * d)/(Sqr(c) + Sqr(d)), (b * c - a * d)/(Sqr(c) + Sqr(d)));
    }

    public double Abs() => Math.Sqrt((double)(Sqr(a) + Sqr(b)));

    public ComplexNumber Conjugate() => new ComplexNumber(a, b * -1);
    
    public ComplexNumber Exp()
    {
        // couldn't decipher instructions, so this is a translation of https://github.com/infusion/Complex.js/blob/master/complex.js#L552
        var expa = Math.Exp(a);
        return new ComplexNumber(expa * Math.Cos(b), expa * Math.Sin(b));
    }
}