module Minesweeper
open System

let annotate (input: string list) = 
    let minesAndAdjancies = 
        let adjacencies point =
            [-1..1] |> List.collect (fun y -> [-1..1] |> List.map (fun x -> (fst point + y, snd point + x)))
            |> List.except [point]
        let forPoint y x c = if c = '*' then ((y,x), adjacencies (y,x) |> Some) else (((y,x)), None)
        input 
            |> List.mapi (fun y line -> line |> Seq.toList |> List.mapi (fun x c -> forPoint y x c))
            |> List.concat |> List.filter (fun (_,a) -> a.IsSome) |> List.map (fun (p,a) -> (p,a.Value))
    let plotter y x c = 
        if c = '*' then '*' else 
            let count = List.filter (fun (_,a) -> List.contains (y,x) a) minesAndAdjancies |> List.length
            if count = 0 then ' ' else count |> string |> char
    input 
        |> List.mapi (fun y line -> line |> Seq.toList |> List.mapi (fun x c -> plotter y x c) |> String.Concat)