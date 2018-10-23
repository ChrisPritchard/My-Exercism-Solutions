using System;
using System.Linq;

public enum YachtCategory
{
    Ones = 1, Twos = 2, Threes = 3, Fours = 4, Fives = 5, Sixes = 6, FullHouse = 7, 
    FourOfAKind = 8, LittleStraight = 9, BigStraight = 10, Choice = 11, Yacht = 12,
}

public static class YachtGame
{
    public static int Score(int[] dice, YachtCategory category)
    {
        switch(category)
        {
            case YachtCategory.FullHouse:
                var fullHouse = dice.GroupBy(d => d).OrderBy(o => o.Count());
                return fullHouse.First().Count() == 2 ? dice.Sum() : 0;
            case YachtCategory.FourOfAKind:
                var fourOfAKind = dice.GroupBy(d => d).OrderBy(o => o.Key);
                return fourOfAKind.Last().Count() >= 4 ? fourOfAKind.Last().Take(4).Sum() : 0;
            case YachtCategory.LittleStraight:
                var littleStraight = dice.GroupBy(d => d).Select(o => o.Key).OrderBy(d => d).ToArray();
                return littleStraight.Length == 5 && littleStraight[4] == 5 ? 30 : 0;
            case YachtCategory.BigStraight:
                var bigStraight = dice.GroupBy(d => d).Select(o => o.Key).OrderBy(d => d).ToArray();
                return bigStraight.Length == 5 && bigStraight[0] == 2 ? 30 : 0;
            case YachtCategory.Choice:
                return dice.Sum();
            case YachtCategory.Yacht:
                var yaught = dice.GroupBy(d => d);
                return yaught.Count() == 1 ? 50 : 0;
            default:
                return dice.Count(d => d == (int)category) * (int)category;
        }
    }
}