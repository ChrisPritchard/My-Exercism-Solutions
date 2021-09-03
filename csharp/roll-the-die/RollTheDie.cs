using System;

public class Player
{
    public int RollDie() => new Random().Next(1,19);

    public double GenerateSpellStrength() => new Random().NextDouble()*100.0;
}
