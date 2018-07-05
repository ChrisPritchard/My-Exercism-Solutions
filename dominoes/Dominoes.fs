module Dominoes

let rec private remove elem lst =
    match lst with
    | h::t when h = elem -> t
    | h::t -> h::remove elem t
    | _ -> []

let rec canChain input = 
    let rec findLast n lst =
         match lst with
         | [] -> Some n
         | _ ->
            List.filter (fun (s,e) -> s = n || e = n) lst
            |> List.map (fun o -> 
                let remainder = remove o lst
                if fst o = n 
                    then findLast (snd o) remainder 
                    else findLast (fst o) remainder)
            |> List.tryPick id
    match input with
    | [] -> true
    | [o] when fst o = snd o -> true
    | h::t -> 
        match findLast (snd h) t with
        | Some n when n = fst h -> true
        | _ -> false
