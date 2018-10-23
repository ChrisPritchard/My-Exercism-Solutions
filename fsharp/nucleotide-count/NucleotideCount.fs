module NucleotideCount
open System.Collections.Generic

let nucleotideCounts (strand: string): Map<char, int> option = 
    let start = [('A',0);('C',0);('G',0);('T',0)] |> Map.ofList
    try
        strand |> Seq.fold (fun acc x -> Map.add x (acc.[x] + 1) acc) start |> Some
    with
    | :? KeyNotFoundException -> None