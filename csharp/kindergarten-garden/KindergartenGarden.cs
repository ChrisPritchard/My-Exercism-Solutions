using System;
using System.Linq;
using System.Collections.Generic;

public enum Plant { Violets, Radishes, Clover, Grass }

public class KindergartenGarden
{    
    private readonly string[] students = new[] 
        { "Alice", "Bob", "Charlie", "David", 
          "Eve", "Fred", "Ginny", "Harriet", 
          "Ileana", "Joseph", "Kincaid", "Larry" };

    private Dictionary<string, IEnumerable<Plant>> byStudent;

    public KindergartenGarden(string diagram)
    {
        diagram = string.Concat(diagram.Split('\n').Select(row => row.PadRight(24)));
        byStudent = 
            Enumerable.Range(0, diagram.Length / 2).SelectMany(i => 
            {
                var student = i >= 12 ? i - 12 : i;
                return new[] { (student, diagram[i * 2]), (student, diagram[i * 2 + 1]) };
            })
            .GroupBy(pair => pair.student)
            .ToDictionary(o => students[o.Key], o => 
                o.Where(pair => pair.Item2 != ' ').Select(pair => AsPlant(pair.Item2)));
    }

    Plant AsPlant(char c)
    {
        switch (c)
        {
            case 'V': return Plant.Violets;
            case 'R': return Plant.Radishes;
            case 'C': return Plant.Clover;
            case 'G': return Plant.Grass;
            default: throw new ArgumentException($"Invalid input '{c}': only V, R, C and G can be accepted");
        }
    }

    public IEnumerable<Plant> Plants(string student) => byStudent[student];
}