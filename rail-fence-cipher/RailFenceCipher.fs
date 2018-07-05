module RailFenceCipher
open System

let private index rails i = 
    let adj = rails - 1
    let index = i % (adj * 2)
    if index > adj then adj - (index - adj) else index      

let private segmentString lengths (input : string) =
    let segments = Seq.zip (Seq.scan (+) 0 lengths) lengths
    Seq.map (input.Substring : int * int -> string) segments

let encode rails (message: string) = 
    message 
        |> Seq.mapi (fun i c -> (index rails i, c))
        |> Seq.groupBy (fun (i,_) -> i) 
        |> Seq.map (fun (_,cs) -> cs |> Seq.map (fun (_,c) -> c) |> String.Concat)
        |> String.Concat

let decode rails (message: string) = 
    let rows = 
        [0..message.Length - 1] 
        |> Seq.map (fun i -> (i, index rails i))
        |> Seq.groupBy (fun (_,l) -> l)
    let segments = message |> segmentString (rows |> Seq.map (fun (_,s) -> Seq.length s))
    let chars = Seq.map2 (fun (_,cs) line -> Seq.map2 (fun (i,_) c -> (i,c)) cs line) rows segments
    chars |> Seq.concat |> Seq.sortBy (fun (i,_) -> i) |> Seq.map snd |> String.Concat
