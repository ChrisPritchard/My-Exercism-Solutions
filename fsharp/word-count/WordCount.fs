module WordCount
open System

let countWords (phrase:string) = 
    let filter (c:char) = System.Char.IsDigit c || System.Char.IsLetter c || c = '\'' || c = ' ' || c = ','
    let clean = phrase.ToLowerInvariant() |> Seq.filter filter |> System.String.Concat
    let words = clean.Split([|' ';','|], StringSplitOptions.RemoveEmptyEntries) |> Array.map (fun x -> x.Trim('\''))
    words |> Array.groupBy id |> Array.map (fun x -> (fst x, Array.length (snd x))) |> Map.ofArray
