module Yacht

type Category = 
    | Ones
    | Twos
    | Threes
    | Fours
    | Fives
    | Sixes
    | FullHouse
    | FourOfAKind
    | LittleStraight
    | BigStraight
    | Choice
    | Yacht

let score category dice = 
    let countDice target = List.filter (fun d -> d = target) dice |> List.length |> (*) target
    let grouped = List.groupBy id dice
    let getSet target = List.tryFind (fun (_,set) -> List.length set >= target)
    match category with
    | Ones -> countDice 1
    | Twos -> countDice 2
    | Threes -> countDice 3
    | Fours -> countDice 4
    | Fives -> countDice 5
    | Sixes -> countDice 6
    | FullHouse -> 
        match (getSet 2 grouped, getSet 3 grouped) with
        | (Some n1, Some n2) when n1 <> n2 -> List.sum dice
        | _ -> 0
    | FourOfAKind -> 
        match grouped |> getSet 4 with
        | Some (key,_) -> key * 4
        | None -> 0
    | LittleStraight -> if List.sort dice = [1..5] then 30 else 0
    | BigStraight -> if List.sort dice = [2..6] then 30 else 0
    | Choice -> List.sum dice
    | Yacht -> if Seq.distinct dice |> Seq.length = 1 then 50 else 0
