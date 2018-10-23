module Transpose
open System

let transpose (input: string list) =
    let padder (lines,length) (line:string) =
        let newLine = line.PadRight(length)
        (lines @ [newLine], newLine.Length)
    let padded = List.fold padder ([], 0) (List.rev input) |> fst |> List.rev

    let revolver i = 
        padded 
        |> List.filter (fun line -> line.Length > i) 
        |> List.map (fun line -> line.[i]) 
        |> String.Concat

    let maxLength = if List.isEmpty padded then 0 else padded.[0].Length
    [0..maxLength - 1] |> List.map revolver
