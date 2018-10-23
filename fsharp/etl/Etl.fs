module Etl

let transform (scoresWithLetters: Map<int, char list>): Map<char, int> = 
    let cs (c:char) = c.ToString().ToLowerInvariant().[0]
    scoresWithLetters |> Seq.collect (fun s -> Seq.map (fun c -> (cs c, s.Key)) s.Value) |> Seq.toList |> Map.ofList