module CryptoSquare

open System

let ciphertext (input:string) = 
    let normalised = 
        input.ToLower() |> Seq.toList 
        |> List.filter (fun (c:char) -> Char.IsLetter c || Char.IsNumber c)
    match normalised with
    | [] -> ""
    | _ -> 
        let cols = float normalised.Length |> sqrt |> ceil |> int
        let chunked = normalised |> List.chunkBySize cols
        let rows = chunked.Length
        let cipher = seq { 
            for i in [0..cols - 1] 
                do yield! Seq.map (fun (chunk: char list) -> if i >= chunk.Length then ' ' else chunk.[i]) chunked
            }
        System.String.Join(" ", cipher |> Seq.chunkBySize rows |> Seq.map System.String.Concat)
