using System;

public class SpiralMatrix
{
    public static int[,] GetMatrix(int size)
    {
        var result = new int[size, size];
        bool isValid(int x, int y) => 
            x >= 0 && y >= 0 && x < size && y < size 
            && result[y, x] == 0;

        (int x, int y, int direction) agent = (0, 0, 0);
        var count = 1;
        while(isValid(agent.x, agent.y))
        {
            result[agent.y, agent.x] = count++;
            var (nextX, nextY) = NextFor(agent.x, agent.y, agent.direction);

            if(isValid(nextX, nextY))
            {
                agent = (nextX, nextY, agent.direction);
                continue;
            }
            
            var nextDir = agent.direction == 3 ? 0 : agent.direction + 1;
            var (turnX, turnY) = NextFor(agent.x, agent.y, nextDir);
            agent = (turnX, turnY, nextDir);
        }

        return result;
    }

    private static (int x, int y) NextFor(int x, int y, int direction)
    {
        if(direction == 0)
            return (x + 1, y);
        if(direction == 1)
            return (x, y + 1);
        if(direction == 2)
            return (x - 1, y);
        return (x, y - 1);
    }
}
