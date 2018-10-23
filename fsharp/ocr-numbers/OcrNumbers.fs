module OcrNumbers

let number input = 
    match input with
    | [" _ ";"| |";"|_|"] -> '0'
    | ["   ";"  |";"  |"] -> '1'
    | [" _ ";" _|";"|_ "] -> '2'
    | [" _ ";" _|";" _|"] -> '3'
    | ["   ";"|_|";"  |"] -> '4'
    | [" _ ";"|_ ";" _|"] -> '5'
    | [" _ ";"|_ ";"|_|"] -> '6'
    | [" _ ";"  |";"  |"] -> '7'
    | [" _ ";"|_|";"|_|"] -> '8'
    | [" _ ";"|_|";" _|"] -> '9'
    | _ -> '?'

let concat (x:char seq) = System.String.Concat x
let join (x:string seq) = System.String.Join(",", x)
let toChars (s: string) = s.ToCharArray() |> Array.toList

let line (rows:string list): string =
    let cells = rows |> List.take 3 |> List.map (toChars >> List.chunkBySize 3 >> (List.map concat))
    let rowLength = cells.[0].Length - 1
    seq { for i in 0..rowLength do yield [cells.[0].[i];cells.[1].[i];cells.[2].[i]] |> number } |> concat

let convert input = 
    match input with 
    | _ when (List.length input) % 4 <> 0 -> None 
    | _ when List.exists (fun (x:string) -> x.Length % 3 <> 0) input -> None
    | _ -> input |> List.chunkBySize 4 |> List.map line |> join |> Some
