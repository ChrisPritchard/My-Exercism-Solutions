module Wordy
open System

let private replace (o:string) (n:string) (str:string)  =
    (str.Replace: string * string -> string) (o, n)

let private split (t:string) (str:string) = 
    (str.Split: string[] * StringSplitOptions -> string[]) ([|t|], StringSplitOptions.RemoveEmptyEntries)

let private (|Int|_|) = 
    System.Int32.TryParse >> function
    | true, n -> Some n
    | false, _ -> None

type private Step =
    | Number of int 
    | Action of (int -> int)

let answer (question:string) = 
    if  not <| question.StartsWith "What is " 
     || not <| question.EndsWith "?" then None
    else
        let body = question.[8..question.Length - 2]
                |> replace "plus" "+" 
                |> replace "minus" "-"
                |> replace "multiplied by" "*"
                |> replace "divided by" "/"
                |> split " "
        let calculator current token =
            match current with
            | None -> None
            | Some (Number n) -> 
                match token with
                | "+" -> Some (Action (fun n2 -> n + n2))
                | "-" -> Some (Action (fun n2 -> n - n2))
                | "*" -> Some (Action (fun n2 -> n * n2))
                | "/" -> Some (Action (fun n2 -> n / n2))
                | _ -> None
            | Some (Action f) -> 
                match token with 
                | Int n -> Some (Number (f n))
                | _ -> None
        Seq.fold calculator (Some (Action id)) body 
            |> function | Some (Number n) -> Some n | _ -> None
