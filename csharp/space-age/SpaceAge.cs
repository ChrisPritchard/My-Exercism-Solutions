using System;
using static System.Math;

public class SpaceAge
{
    private readonly double earthAge;

    public SpaceAge(long seconds) => this.earthAge = Round(seconds / 31557600.0, 2);

    public double OnEarth() => earthAge;

    public double OnMercury() => Round(earthAge / 0.2408467, 2);

    public double OnVenus() => Round(earthAge / 0.61525, 2);

    public double OnMars() => Round(earthAge / 1.8808158, 2);

    public double OnJupiter() => Round(earthAge / 11.862615, 2);

    public double OnSaturn() => Round(earthAge / 29.447498, 2);

    public double OnUranus() => Round(earthAge / 84.016846, 2);

    public double OnNeptune() => Round(earthAge / 164.79132, 2);
}