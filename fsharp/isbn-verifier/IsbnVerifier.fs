module IsbnVerifier
open System

let isValid (isbn:string) = 
    let folder c acc =
        match c with 
        |'X' -> 10::acc
        | _ when Char.IsDigit c -> (int (c.ToString()))::acc
        | _ -> acc
    let digits = Seq.foldBack folder isbn [] |> List.rev
    match digits with
    | _ when digits.Length <> 10 -> false
    | _ when digits |> List.tail |> List.exists (fun x -> x < 0 || x > 9) -> false
    | _ -> 
        let sum = List.mapi (fun i x -> x * (i + 1)) digits |> List.sum
        sum % 11 = 0