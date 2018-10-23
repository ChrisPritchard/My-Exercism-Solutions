module Luhn
open System

let valid number = 
    if Seq.exists (fun c -> not (Char.IsNumber c || c = ' ')) number then false
    else
        let clean = number |> Seq.filter Char.IsNumber |> Seq.map (string >> int)
        if Seq.length clean = 1 then false
        else
            let multiply i n = 
                match i % 2 with 
                | 1 when n < 5 -> n * 2 
                | 1 -> (n * 2) - 9 
                | _ -> n
            let sum = clean |> Seq.rev |> Seq.mapi multiply |> Seq.sum
            sum % 10 = 0
