using System;
using System.Linq;

public enum Direction
{
    North, East, South, West
}

public struct Coordinate
{
    public Coordinate(int x, int y) => 
        (X, Y) = (x, y);

    public int X { get; }
    public int Y { get; }
}

public class RobotSimulator
{
    public RobotSimulator(Direction direction, Coordinate coordinate) => 
        (Direction, Coordinate) = (direction, coordinate);

    public Direction Direction { get; private set; }

    public Coordinate Coordinate { get; private set; }

    public void TurnRight()
    {
        var next = (int)Direction + 1;
        Direction = (Direction)(next % 4);
    }

    public void TurnLeft()
    {
        var next = (int)Direction - 1;
        Direction = (Direction)(next < 0 ? 3 : next);
    }

    public void Advance()
    {
        if(Direction == Direction.North)
            Coordinate = new Coordinate(Coordinate.X, Coordinate.Y + 1);
        else if(Direction == Direction.East)
            Coordinate = new Coordinate(Coordinate.X + 1, Coordinate.Y);
        else if(Direction == Direction.South)
            Coordinate = new Coordinate(Coordinate.X, Coordinate.Y - 1);
        else
            Coordinate = new Coordinate(Coordinate.X - 1, Coordinate.Y);
    }

    public void Simulate(string instructions) => 
        instructions.ToList().ForEach(c => 
        {
            if(c == 'A')
                Advance();
            else if(c == 'L')
                TurnLeft();
            else if(c == 'R')
                TurnRight();
            else
                throw new ArgumentException($"An invalid instruction '{c}' was encountered. Instructions can only be 'A', 'L' or 'R'.");
        });
}