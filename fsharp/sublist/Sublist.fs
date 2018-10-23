module Sublist

type Sublist = Equal | Sublist | Superlist | Unequal

let private isSublist l1 l2 = 
    List.isEmpty l1
    || (List.length l1 < List.length l2 
        && List.windowed l1.Length l2 |> List.contains l1)

let sublist xs ys = 
    if xs = ys then Equal
    elif isSublist ys xs then Superlist
    elif isSublist xs ys then Sublist
    else Unequal