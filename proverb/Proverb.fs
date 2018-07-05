module Proverb

let recite (input: string list): string list = 
    let reciter acc x = 
        match acc with 
        | "" -> ("", x) 
        | _ -> (sprintf "For want of a %s the %s was lost." acc x, x)
    match input with
    | [] -> []
    | _ -> input |> List.mapFold reciter "" |> fst |> List.skip 1 |> (fun x -> x @ [sprintf "And all for the want of a %s." input.[0]])
