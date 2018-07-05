module Alphametics

open System
open FParsec

let psymbols: Parser<string, unit> = many (anyOf ['A'..'Z']) |>> String.Concat
let pexpression = 
    psymbols .>>. many (pstring " + " >>. psymbols) .>> pstring " == " .>>. psymbols
    |>> fun ((i, il), o) -> ([i] @ il, o) 

let numberFrom (map:Map<char,int>) str = 
    Seq.fold (fun (state: string) c -> 
    let found  = map.[c] |> string
    state.Replace (string c, found)) str str
    |> Int64.Parse

let rec bruteForce map remainingChars nonZeros remainingNumbers expression =
    match remainingChars with
    | [] -> 
        let expected = snd expression |> numberFrom map
        if Seq.sumBy (numberFrom map) (fst expression) = expected
        then Some map else None
    | h::tail ->
        let forcer n = 
            let newMap = map.Add (h, n)
            let newNumbers = List.except [n] remainingNumbers
            bruteForce newMap tail nonZeros newNumbers expression
        let validNumbers = 
            if List.contains h nonZeros 
            then List.except [0] remainingNumbers 
            else remainingNumbers
        validNumbers 
        |> List.map forcer
        |> List.tryPick id

let solve input = 
    match run pexpression input with
    | Failure _ -> None
    | Success (parsed,_,_) ->
        let chars =
            String.Concat (fst parsed @ [snd parsed])
            |> Seq.distinct |> Seq.toList
        let nonZeros = 
            fst parsed @ [snd parsed] 
            |> Seq.map (fun w -> w.[0]) |> Seq.distinct |> Seq.toList
        bruteForce Map.empty chars nonZeros [0..9] parsed
