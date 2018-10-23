module BracketPush

let isPaired input = 
    let pairs = [('{','}');('[',']');('(',')')]
    let folder (state: char list) c =
        match c with
        | _ when List.exists (fst >> (=) c) pairs -> c::state
        | _ when List.exists (snd >> (=) c) pairs ->
            let valid = state <> [] 
                        && List.contains (Seq.head state, c) pairs
            if valid then List.tail state else c::state
        | _ -> state
    Seq.fold folder [] input |> List.isEmpty