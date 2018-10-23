using System;
using System.Linq;
using System.Collections.Generic;

public static class BookStore
{
    public static double Total(IEnumerable<int> books)
    {
        var marked = books.Select((o, i) => (o, i));
        return Enumerable
            .Range(2, 4)
            .Select(maxSetSize => Pricer(maxSetSize, marked))
            .Min();
    }

    private static Dictionary<int, double> discounts = new Dictionary<int, double>
        { {1, 0.0}, {2, 0.05}, {3, 0.1}, {4, 0.2}, {5, 0.25} };

    private static double Price(int distinctCount) => 
        distinctCount * 8.0 * (1.0 - discounts[distinctCount]);

    private static double Pricer(int maxSetSize, IEnumerable<(int book, int index)> books)
    {
        var total = 0.0;
        while(books.Any())
        {
            var distinct = books
                .GroupBy(o => {
                    var (book, _) = o;
                    return book;
                })
                .Select(group => group.First())
                .Take(maxSetSize)
                .ToList();
            total += Price(distinct.Count);
            books = books.Except(distinct);
        }
        return total;
    }
}