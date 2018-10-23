using System;
using System.Linq;

public static class Rectangles
{
    public static int Count(string[] rows) =>
        Enumerable.Range(0, rows.Length).SelectMany(y => 
            Enumerable.Range(0, rows[0].Length).Where(x => rows[y][x] == '+')
                .Select(x => CountDown(x, y, rows)))
                .Sum();

    private static int CountDown(int x, int y, string[] rows)
    {
        var result = 0;
        for(var dy = y + 1; dy < rows.Length; dy ++)
            if(rows[dy][x] == '+')
                result += CountRight(x, dy, y, rows);
            else if(rows[dy][x] != '|')
                break;
        return result;
    }

    private static int CountRight(int x, int dy, int oy, string[] rows)
    {
        var result = 0;
        for(var dx = x + 1; dx < rows[0].Length; dx ++)
            if(rows[dy][dx] == '+')
                result += CountUp(dx, dy, x, oy, rows);
            else if(rows[dy][dx] != '-')
                break;
        return result;
    }

    private static int CountUp(int dx, int dy, int ox, int oy, string[] rows)
    {
        for(dy = dy - 1; dy >= oy; dy --)
            if(dy == oy && rows[dy][dx] == '+')
                return CountLeft(dx, oy, ox, rows);
            else if(!"|+".Contains(rows[dy][dx]))
                break;
        return 0;
    }

    private static int CountLeft(int dx, int oy, int ox, string[] rows)
    {
        for(dx = dx - 1; dx >= ox; dx --)
            if(dx == ox)
                return 1;
            else if(!"-+".Contains(rows[oy][dx]))
                break;
        return 0;
    }
}