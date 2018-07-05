module ParallelLetterFrequency
open System

let frequency texts = 
    let findInText (text:string) = async { 
            return text.ToLower() |> Seq.filter (Char.IsLetter) |> Seq.groupBy id |> Seq.map (fun x -> (fst x, Seq.length <| snd x))
        }
    let groupings = texts |> Seq.map findInText |> Async.Parallel |> Async.RunSynchronously
    let combine map (key,num) = 
        if map |> Map.containsKey key 
            then map |> Map.add key (num + map.[key])
            else map |> Map.add key num
    Seq.fold (fun finalMap g -> Seq.fold combine finalMap g) Map.empty groupings
