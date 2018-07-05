module FoodChain

let pairs = [
    ("","");
    ("fly","I don't know why she swallowed the fly. Perhaps she'll die.");
    ("spider","It wriggled and jiggled and tickled inside her.");
    ("bird","How absurd to swallow a bird!");
    ("cat","Imagine that, to swallow a cat!");
    ("dog","What a hog, to swallow a dog!");
    ("goat","Just opened her throat and swallowed a goat!");
    ("cow","I don't know how she swallowed a cow!");
    ("horse","She's dead, of course!");
]

let catchName i = match fst pairs.[i] with | "spider" -> "spider that wriggled and jiggled and tickled inside her" | s -> s

let rec recite (start:int) (stop:int): string list = 
    seq {
        let pair = pairs.[start]
        yield sprintf "I know an old lady who swallowed a %s." (fst pair)
        yield snd pair
        if start <> 1 && start <> 8 then
            yield! [start-1..-1..1] |> List.map (fun i -> sprintf "She swallowed the %s to catch the %s." (fst pairs.[i+1]) (catchName i))
            yield snd pairs.[1]
        match stop with
        | _ when stop = start -> ()
        | _ -> 
            yield ""
            yield! recite (start + 1) stop
    } |> Seq.toList
