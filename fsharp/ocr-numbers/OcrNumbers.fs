module OcrNumbers

let number = 
    function
    | [" _ ";"| |";"|_|";"   "] -> '0'
    | ["   ";"  |";"  |";"   "] -> '1'
    | [" _ ";" _|";"|_ ";"   "] -> '2'
    | [" _ ";" _|";" _|";"   "] -> '3'
    | ["   ";"|_|";"  |";"   "] -> '4'
    | [" _ ";"|_ ";" _|";"   "] -> '5'
    | [" _ ";"|_ ";"|_|";"   "] -> '6'
    | [" _ ";"  |";"  |";"   "] -> '7'
    | [" _ ";"|_|";"|_|";"   "] -> '8'
    | [" _ ";"|_|";" _|";"   "] -> '9'
    | _ -> '?'

let join (chars: seq<char>) = System.String.Concat chars

let line =
    List.map (Seq.chunkBySize 3 >> Seq.map join) 
    >> Seq.transpose
    >> Seq.map (Seq.toList >> number)
    >> join

let convert input = 
    match input with 
    | _ when List.length input % 4 <> 0 -> None 
    | _ when List.exists (fun (x:string) -> x.Length % 3 <> 0) input -> None
    | _ -> input |> List.chunkBySize 4 |> List.map line |> String.concat "," |> Some