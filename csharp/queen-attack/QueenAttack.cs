using System;
using static System.Math;

public class Queen
{
    public Queen(int row, int column) => (Row, Column) = (row, column);

    public int Row { get; }
    public int Column { get; }
}

public static class QueenAttack
{
    public static bool CanAttack(Queen white, Queen black)
        => white.Column == black.Column || white.Row == black.Row
        || Abs(white.Column - black.Column) == Abs(white.Row - black.Row);

    public static Queen Create(int row, int column)
    {
        if (row < 0 || row > 7 || column < 0 || column > 7)
            throw new ArgumentOutOfRangeException($"Both {nameof(row)} and {nameof(column)} must be between 0 and 7 inclusive");
            
        return new Queen(row, column);
    }
}