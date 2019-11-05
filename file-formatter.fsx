open System
open System.IO

let formatFile path = 
    let lines = File.ReadAllLines path
    if Array.windowed 3 lines |> Array.exists (Array.forall String.IsNullOrWhiteSpace) then
        let removeErrantLines (validset, wscount) line = 
            if String.IsNullOrWhiteSpace line |> not then (line::validset, 0)
            elif  wscount = 2 then (line::validset, 0)
            else (validset, wscount + 1)

        let newLines = Array.fold removeErrantLines ([],0) lines
        printfn "Formatted %s" <| Path.GetFileName(path)
        File.WriteAllLines (path, fst newLines |> List.rev)

printf "enter path: "
let path = Console.ReadLine ()

if not (Directory.Exists path) then
    printfn "Invalid path: directory could not be found"
else
    let files = Directory.GetFiles(path, "*.fs", SearchOption.AllDirectories)
    files |> Array.iter formatFile