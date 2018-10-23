using System;
using System.Collections.Generic;
using System.Linq;

public enum Color { Red , Green , Ivory , Yellow , Blue }
public enum Nationality { Englishman , Spaniard , Ukranian , Japanese , Norwegian }
public enum Pet { Dog , Snails , Fox , Horse , Zebra }
public enum Drink { Coffee , Tea , Milk , OrangeJuice , Water }
public enum Smoke { OldGold , Kools , Chesterfields , LuckyStrike , Parliaments }

public static class ZebraPuzzle
{
    private enum House { FarLeft , Left , Middle , Right , FarRight }
    private enum Neighbour { LeftOf, RightOf, Either }

    private class Person
    {
        public Color HousesColour { get; }
        public Nationality From { get; }
        public Pet Owns { get; }
        public Drink Drinks { get; }
        public Smoke Smokes { get; }
        public House LivesAt { get; }

        public Person(
            Color colour, Nationality nationality, 
            Pet pet, Drink drinks, Smoke smokes, House house) => 
            (HousesColour, From, Owns, Drinks, Smokes, LivesAt) = 
            (colour, nationality, pet, drinks, smokes, house);

        public bool NoConflict(Person other) =>
            HousesColour != other.HousesColour
            && From != other.From
            && Owns != other.Owns
            && Drinks != other.Drinks
            && Smokes != other.Smokes
            && LivesAt != other.LivesAt;
    }

    private static readonly (object subject, object fact, bool isTrue)[] rules = 
        new (object, object, bool)[]
        {
            (Nationality.Englishman, Color.Red, true), // 2
            (Nationality.Spaniard, Pet.Dog, true), // 3
            (Drink.Coffee, Color.Green, true), // 4
            (Nationality.Ukranian, Drink.Tea, true), // 5
            (Smoke.OldGold, Pet.Snails, true), // 7
            (Smoke.Kools, Color.Yellow, true), // 8
            (Drink.Milk, House.Middle, true), // 9
            (Nationality.Norwegian, House.FarLeft, true), // 10
            (Smoke.Chesterfields, Pet.Fox, false), // 11
            (Smoke.Kools, Pet.Horse, false), // 12
            (Smoke.LuckyStrike, Drink.OrangeJuice, true), // 13
            (Nationality.Japanese, Smoke.Parliaments, true), // 14
            (Nationality.Norwegian, Color.Blue, false) // 15
        };

    private static readonly (object subject, object fact, Neighbour relation)[] neighbourRules = 
        new (object, object, Neighbour)[]
        {
            (Color.Green, Color.Ivory, Neighbour.RightOf), // 6
            (Smoke.Chesterfields, Pet.Fox, Neighbour.Either), // 11
            (Smoke.Kools, Pet.Horse, Neighbour.Either), // 12
            (Nationality.Norwegian, Color.Blue, Neighbour.Either) // 15
        };

    private static IEnumerable<Person> AllPossibilities()
    {
        IEnumerable<int> range() => Enumerable.Range(0, 5);

        var possibilities = 
            range()
                .SelectMany(c => range()
                    .SelectMany(n => range()
                        .SelectMany(p => range()
                            .SelectMany(d => range()
                                .SelectMany(s => range()
                                    .Select(h => 
                new Person(
                    (Color)c, (Nationality)n, (Pet)p, 
                    (Drink)d, (Smoke)s, (House)h)))))));

        foreach(var possible in possibilities)
            yield return possible;
    }

    private static bool Applies(Person person, object aspect)
    {
        if (aspect is Color)
            return person.HousesColour == (Color)aspect;
        if (aspect is Nationality)
            return person.From == (Nationality)aspect;
        if (aspect is Pet)
            return person.Owns == (Pet)aspect;
        if (aspect is Drink)
            return person.Drinks == (Drink)aspect;
        if (aspect is Smoke)
            return person.Smokes == (Smoke)aspect;
        return person.LivesAt == (House)aspect;
    }

    private static bool TestRule(Person person, (object subject, object fact, bool isTrue) rule)
    {
        if(Applies(person, rule.subject))
            return rule.isTrue && Applies(person, rule.fact) || !rule.isTrue && !Applies(person, rule.fact);
        else
            return rule.isTrue && !Applies(person, rule.fact) || !rule.isTrue;
    }

    private static bool TestRule(IEnumerable<Person> set, (object subject, object target, Neighbour fact) rule)
    {
        var sorted = set.OrderBy(o => o.LivesAt);
        var pairs = sorted.Zip(sorted.Skip(1), (a, b) => (left: a, right: b));
        if(rule.fact == Neighbour.LeftOf)
            return pairs.Any(o => Applies(o.left, rule.subject) && Applies(o.right, rule.target));
        if(rule.fact == Neighbour.RightOf)
            return pairs.Any(o => Applies(o.right, rule.subject) && Applies(o.left, rule.target));
        
        return TestRule(set, (rule.subject, rule.target, Neighbour.LeftOf))
            || TestRule(set, (rule.subject, rule.target, Neighbour.RightOf));
    }

    private static IEnumerable<IEnumerable<Person>> DistinctSets(IEnumerable<Person> soFar, IEnumerable<Person> candidates)
    {
        if(soFar.Count() == 5)
        {
            yield return soFar;
            yield break;
        }
        
        var subSets = candidates
            .Where(candidate => !soFar.Contains(candidate) 
                && soFar.All(existing => existing.NoConflict(candidate)))
            .SelectMany(candidate => DistinctSets(soFar.Append(candidate), candidates).Distinct())
            .Distinct();

        foreach(var subSet in subSets)
            yield return subSet;
    }

    private static IEnumerable<Person> Solve()
    {
        var candidates = AllPossibilities().Where(person => rules.All(rule => TestRule(person, rule)));
        var sets = DistinctSets(Enumerable.Empty<Person>(), candidates);
        return sets.First(set => neighbourRules.All(rule => TestRule(set, rule)));
    }

    public static Nationality DrinksWater()
        => Solve().Single(person => person.Drinks == Drink.Water).From;
    
    public static Nationality OwnsZebra()
        => Solve().Single(person => person.Owns == Pet.Zebra).From;
}