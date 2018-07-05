module VariableLengthQuantity
open System.Collections

let asBits length number =
    [0..length - 1] |> List.map (fun n -> (number >>> n) &&& 1u |> (=) 1u) |> List.rev

let pad length bits =
    if List.length bits <> length then 
        ([1..length - List.length bits] |> List.map (fun _ -> false)) @ bits 
    else bits

let asVLQ bits =
    bits |> List.rev |> List.chunkBySize 7 |> List.map (List.rev >> pad 7) |> List.rev
         |> List.skipWhile (fun b -> b |> List.forall not)
         |> fun bytes -> 
            let prefix i = i < bytes.Length - 1
            List.indexed bytes |> List.map (fun (i,b) -> (prefix i)::b)

let asBytes (bits: bool list list) =
    match bits with
    | [] -> [0uy]
    | _ ->
        bits |> List.map (fun byte -> 
            let target = Array.create 1 0uy
            let bitArray = new BitArray (byte |> List.rev |> List.toArray)
            bitArray.CopyTo(target, 0)
            target.[0])

let fromVLQ bits =
    let processed = 
        List.chunkBySize 8 bits 
        |> List.fold (fun (all,current) byte -> 
            if byte.Head then (all, current @ byte.Tail)
            else (all @ [current @ byte.Tail],[])) ([],[])
    match processed with
    | (result,[]) -> result |> List.map (List.skipWhile not >> pad 32) |> Some
    | _ -> None

let asBase10 bits = 
    bits |> Seq.map (fun x -> if x then 1u else 0u) 
         |> Seq.rev |> Seq.mapi (fun i x -> pown 2u i |> (*) x) 
         |> Seq.sum

let encode (numbers: uint32 list): byte list =
    numbers |> List.collect (asBits 32 >> asVLQ >> asBytes)

let decode (bytes: byte list) =
    bytes |> List.map uint32 
          |> List.collect (asBits 8) |> fromVLQ 
          |> Option.bind (fun result -> result |> List.map asBase10 |> Some)
