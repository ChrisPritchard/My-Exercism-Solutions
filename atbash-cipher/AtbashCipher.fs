module AtbashCipher
open System

let cipherSet = List.map2 (fun x y -> (x, y)) ['a'..'z'] (List.rev ['a'..'z']) |> Map.ofList
let cipher c = match cipherSet |> Map.containsKey c with | true -> cipherSet.[c] | false -> c
let validChars (str:string) = str.ToLower() |> Seq.filter (fun c -> Char.IsLetter c || Char.IsNumber c)

let encode (str:string) = 
    let ciphered = validChars str |> Seq.map cipher |> Seq.chunkBySize 5 |> Seq.map String.Concat
    String.Join(" ", ciphered)

let decode (str:string) = str.Replace(" ", "") |> Seq.map cipher |> String.Concat
