module House

let pattern = 
    [
        ("house","lay in")
        ("malt", "ate");
        ("rat", "killed");
        ("cat", "worried");
        ("dog","tossed");
        ("cow with the crumpled horn","milked");
        ("maiden all forlorn","kissed");
        ("man all tattered and torn","married");
        ("priest all shaven and shorn","woke");
        ("rooster that crowed in the morn","kept");
        ("farmer sowing his corn","belonged to");
        ("horse and the hound and the horn","")
    ]

let recite startVerse endVerse: string list =
    let lines x = 
        seq { 
            for i in [x-1..-1..0] do 
                yield 
                    match i with
                    | _ when i = 0 && x = 1 -> "This is the house that Jack built."
                    | _ when i = 0 -> "that lay in the house that Jack built."
                    | _ when i = x-1 -> sprintf "This is the %s" (fst pattern.[i])
                    | _ -> sprintf "that %s the %s" (snd pattern.[i]) (fst pattern.[i])
            if x <> endVerse then yield ""
        }
    [startVerse..endVerse] |> Seq.collect lines |> Seq.toList