module Grep
open System.IO

let grep files flagArguments pattern = 
    let arg x = List.contains x flagArguments
    let lower (s:string) = if arg "-i" then s.ToLower() else s
    let matcher (_,text) = 
        let pattern = lower pattern
        let text = lower text
        let valid = if arg "-x" then text = pattern else text.Contains pattern
        if arg "-v" then not valid else valid
    let format file (num,text) = 
        match flagArguments with
        | _ when arg "-l" -> file
        | _ ->
            [
                (if List.length files > 1 then file + ":" else "");
                (if arg "-n" then num.ToString() + ":" else "");
                text
            ] |> System.String.Concat
    let rec innerGrep files flagArguments pattern = 
        seq {
            let file = List.head files
            let lines = File.ReadAllLines file |> Seq.mapi (fun i l -> (i+1,l))
            yield! lines |> Seq.filter matcher |> Seq.map (format file)
            match List.tail files with
            | [] -> ()
            | rest -> yield! innerGrep rest flagArguments pattern
        } |> Seq.distinct |> Seq.toList
    innerGrep files flagArguments pattern