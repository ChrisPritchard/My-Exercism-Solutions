module Poker
open System

let private formatHand (hand: string) = 
    hand.Split(' ') |> Array.toList |> List.map (fun c -> 
    (Seq.take (c.Length - 1) c |> String.Concat, Seq.last c))

let private rankCard =
    function
    | "A" -> 14
    | "K" -> 13
    | "Q" -> 12
    | "J" -> 11
    | n -> Int32.Parse n

type Hand = private | HighCard of int list | Pair of int | TwoPair of int * int | ThreeOfAKind of int
            | Straight of int list | Flush of int list | FullHouse of int * int | FourOfAKind of int
            | StraightFlush of int list | Kicker of int

let private rankHand (hand: (string * char) list) =
    let grouped = hand |> List.groupBy fst
    let has n = 
        grouped |> List.filter (fun p -> snd p |> List.length = n) 
        |> List.length
    let get n = 
        grouped |> List.filter (fun p -> snd p |> List.length = n) 
        |> List.map (fst >> rankCard) |> List.sortByDescending id
    let single n = get n |> List.head
    let pair n = get n |> (fun lst -> (lst.[0],lst.[1]))
    let remainder = get 1 |> List.map Kicker

    if has 2 = 1 && has 3 = 0 then (Pair (single 2), remainder)
    elif has 2 = 2 then (TwoPair (pair 2), remainder)
    elif has 3 = 1 && has 2 = 0 then (ThreeOfAKind (single 3), remainder)
    elif has 3 = 1 && has 2 = 1 then (FullHouse (single 3, single 2), [])
    elif has 4 = 1 then (FourOfAKind (single 4), remainder)
    else
        let isStraight = 
            let ranked = hand |> List.map (fst >> rankCard) |> List.sort
            let diff = (List.last ranked) - (List.head ranked)
            diff = 4 || diff = 12
        let isFlush = hand |> List.map snd |> List.distinct |> List.length |> (=) 1

        let swapAce lst = 
            if List.contains 5 lst then 
                List.map (fun n -> if n = 14 then 1 else n) lst 
                else lst

        if isFlush && isStraight then (StraightFlush (get 1 |> swapAce), [])
        elif isFlush then (Flush (get 1), [])
        elif isStraight then (Straight (get 1 |> swapAce), [])
        else
            (HighCard (get 1), [])

let bestHands (hands:string list): string list =
    hands |> List.map (fun h -> (h, formatHand h |> rankHand))
          |> List.sortByDescending snd |> List.groupBy snd |> List.head |> snd
          |> List.map fst
