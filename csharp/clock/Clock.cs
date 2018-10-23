using System;

public struct Clock
{
    private readonly int totalMinutes;

    const int minutesInDay = 24 * 60;

    public Clock(int hours, int minutes)
    {
        totalMinutes = (hours * 60 + minutes) % minutesInDay;
        if (totalMinutes < 0) totalMinutes += minutesInDay;
    }

    public int Hours => totalMinutes / 60;

    public int Minutes => totalMinutes % 60;

    public Clock Add(int minutesToAdd) => new Clock(0, totalMinutes + minutesToAdd);

    public Clock Subtract(int minutesToSubtract) => new Clock(0, totalMinutes - minutesToSubtract);

    public override string ToString() => $"{Hours:00}:{Minutes:00}";
}