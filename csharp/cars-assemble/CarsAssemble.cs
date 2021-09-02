using System;

static class AssemblyLine
{
    public static double ProductionRatePerHour(int speed)
    {
        var total = (double)(speed * 221);
        if (speed == 10)
            return total * 0.77;
        else if (speed == 9)
            return total * 0.8;
        else if (speed > 4)
            return total * 0.9;
        else
            return total;
    }

    public static int WorkingItemsPerMinute(int speed) => (int)(ProductionRatePerHour(speed) / 60);
}
