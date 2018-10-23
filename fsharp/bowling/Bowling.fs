module Bowling

type Frame =
    | Strike
    | Spare of int * int
    | Normal of int * int
    | Incomplete of int

type Game =
    | ValidGame of Frame list
    | InvalidGame

let newGame() = ValidGame []

let (|IsFinished|NotFinished|) (allRolls:Game) =
    match allRolls with
    | InvalidGame -> IsFinished
    | ValidGame r -> 
        match List.length r with
        | 10 ->
            match r.[9] with
            | Spare _ | Strike | Incomplete _ -> NotFinished
            | _ -> IsFinished
        | 11 ->
            match r.[9] with
            | Incomplete _ | Normal _ -> failwith "Invalid game"
            | Spare _ -> IsFinished
            | Strike ->
                match r.[10] with 
                | Incomplete _ -> NotFinished
                | Strike -> NotFinished
                | _ -> IsFinished
        | 12 ->
            match r.[9] with
            | Incomplete _ | Normal _ | Spare _ -> failwith "Invalid game"
            | Strike ->
                match r.[10] with 
                | Incomplete _ -> NotFinished
                | _ -> IsFinished
        | _ -> NotFinished

let roll pins rolls = 
    match rolls with
    | InvalidGame -> InvalidGame
    | IsFinished -> InvalidGame
    | _ when pins < 0 || pins > 10 -> InvalidGame
    | ValidGame s -> 
        let last = List.tryLast s
        let from pins = match pins with | 10 -> Strike | n -> Incomplete n
        match last with
        | Some Strike -> ValidGame (s @ [from pins])
        | Some (Incomplete n) when n + pins > 10 -> InvalidGame
        | Some (Incomplete n) -> 
            let exceptLast = List.take (List.length s - 1) s
            let newFrame = if n + pins = 10 then Spare (n,pins) else Normal (n,pins)
            ValidGame (exceptLast @ [newFrame])
        | _ -> ValidGame (s @ [from pins])

let scorer (score,doubles) frame =
    match frame with
    | Strike ->
        match doubles with
        | [3;2] -> (score + 30, [3;2])
        | [2;2] -> (score + 20, [3;2])
        | [2] -> (score + 20, [2;2])
        | _ -> (score + 10, [2;2])
    | Spare (r1, r2) ->
        match doubles with
        | [3;2] -> (score + (r1*3) + (r2*2), [2])
        | [2;2] -> (score + (r1*2) + (r2*2), [2])
        | [2] -> (score + (r1*2) + r2, [2])
        | _ -> (score + 10, [2])
    | Normal (r1, r2) ->
        match doubles with
        | [3;2] -> (score + (r1*3) + (r2*2), [])
        | [2;2] -> (score + (r1*2) + (r2*2), [])
        | [2] -> (score + (r1*2) + r2, [])
        | _ -> (score + r1 + r2, [])
    | _ -> failwith "Invalid game" // shouldnt be reachable

let rawScore frame =
    match frame with
    | Strike -> 10
    | Spare (r1, r2) -> r1 + r2
    | Normal (r1, r2) -> r1 + r2
    | Incomplete n -> n

let score game =
    match game with
    | InvalidGame -> None
    | NotFinished -> None
    | ValidGame rolls ->
        let score = List.fold scorer (0,[]) (List.truncate 10 rolls)
        let bonus = // adjustment for how doubles are calculated and a streak of strikes
            match snd score with
            | [3;_] -> 10
            | _ -> 0
        match rolls.Length with
        | 12 -> fst score + bonus + rawScore rolls.[10] + rawScore rolls.[11] |> Some
        | 11 -> fst score + bonus + rawScore rolls.[10] |> Some
        | _ -> fst score |> Some