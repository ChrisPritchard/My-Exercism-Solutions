using System;
using System.Collections.Generic;
using System.Linq;

public static class Poker
{
    public static IEnumerable<string> BestHands(IEnumerable<string> hands)
    {
        var sorted = 
            hands.Select(Hand).Select(Score)
            .OrderByDescending(o => o.handType)
            .ThenByDescending(o => Sortable(o.score))
            .ThenByDescending(o => Sortable(o.kickers));
        var grouped = 
            sorted.GroupBy(o => o.handType)
            .Select(o => o.GroupBy(t => 
                Sortable(t.score) + Sortable(t.kickers)));

        return grouped.First().First().Select(o => o.source);
    }

    private static string Sortable(IEnumerable<int> source)
        => string.Concat(source.Select(o => o.ToString().PadLeft(2, '0')));

    private enum HandType 
    {
        StraightFlush = 128, FourOfAKind = 64, FullHouse = 32, Flush = 16, Straight = 8, ThreeOfAKind = 4, TwoPair = 2, OnePair = 1, HighCard = 0
    }

    private static int Rank(char rank)
    {
        switch(rank)
        {
            case 'A': return 14;
            case 'K': return 13;
            case 'Q': return 12;
            case 'J': return 11;
            default: return rank - '0';
        }
    }

    private static (string source, IEnumerable<(int rank, char suit)>) Hand(string source)
        => (source, source.Split(' ')
            .Select(card => card.Length == 3
                ? (rank: 10, card[2])
                : (rank: Rank(card[0]), card[1]))
            .OrderByDescending(o => o.rank));

    private static (string source, HandType handType, int[] score, int[] kickers) Score((string source, IEnumerable<(int rank, char suit)> cards) hand)
    {
        var grouped = hand.cards.GroupBy(o => o.rank).OrderByDescending(o => o.Count()).ToArray();
        var counts = grouped.Select(o => o.Count()).ToArray();
        var ranks = grouped.Select(o => o.Key).ToArray();

        if(counts.Length == 2 && counts[0] == 4)
            return (hand.source,
                HandType.FourOfAKind, 
                new [] { ranks[0] }, 
                new [] { ranks[1] });
        if(counts.Length == 2)
            return (hand.source,
                HandType.FullHouse, 
                new [] { ranks[0], ranks[1] }, 
                new int[0]);
        if(counts.Length == 3 && counts[0] == 3)
            return (hand.source,
                HandType.ThreeOfAKind, 
                new [] { ranks[0] }, 
                new [] { ranks[1], ranks[2] });
        if(counts.Length == 3)
            return (hand.source,
                HandType.TwoPair, 
                new [] { ranks[0], ranks[1] }, 
                new [] { ranks[2] });
        if(counts.Length == 4)
            return (hand.source,
                HandType.OnePair, 
                new [] { ranks[0] }, 
                new [] { ranks[1], ranks[2], ranks[3] });

        var aceLow = ranks[0] == 14 && ranks[4] == 2 && ranks[1] - ranks[4] == 3;
        var isDescending = ranks[0] - ranks[4] == 4 || aceLow;
        var newRanks = isDescending && aceLow ? ranks.Select(o => o == 14 ? 1 : o).ToArray() : ranks;
        var isSameSuit = hand.cards.GroupBy(o => o.suit).Count() == 1;

        if(isDescending && isSameSuit)
            return (hand.source, HandType.StraightFlush, newRanks, new int [0]);
        if(isDescending)
            return (hand.source, HandType.Straight, newRanks, new int [0]);
        if(isSameSuit)
            return (hand.source, HandType.Flush, ranks, new int [0]);
        
        return (hand.source, HandType.HighCard, ranks, new int [0]);
    }
}