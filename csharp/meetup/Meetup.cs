using System;
using System.Collections.Generic;
using System.Linq;

public enum Schedule
{
    Teenth, First, Second, Third, Fourth, Last
}

public class Meetup
{
    private readonly int month;
    private readonly int year;

    public Meetup(int month, int year) => (this.month, this.year) = (month, year);

    public DateTime Day(DayOfWeek dayOfWeek, Schedule schedule)
    {
        DateTime findDay(IEnumerable<int> candidates) => 
            candidates
                .Select(d => new DateTime(year, month, d))
                .Last(d => d.DayOfWeek == dayOfWeek);
        
        IEnumerable<int> range(int min, int max) => Enumerable.Range(min, max - (min - 1));

        switch(schedule)
        {
            case Schedule.First:
                return findDay(range(1, 7));
            case Schedule.Second:
                return findDay(range(7, 14));
            case Schedule.Third:
                return findDay(range(14, 21));
            case Schedule.Fourth:
                return findDay(range(21, 28));
            case Schedule.Last:
                var lastDay = (new DateTime(year, month, 1)).AddMonths(1).AddDays(-1).Day;
                return findDay(range(21, lastDay));
            default:
                return findDay(range(13, 19));
        }
    }
}