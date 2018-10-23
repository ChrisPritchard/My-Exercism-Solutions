module Rectangles

let private subsets size input = 
    let rec inner elems = 
        match elems with
        | [] -> [[]]
        | h::t -> 
            List.fold (fun acc e -> 
                if List.length e < size 
                    then (h::e)::e::acc 
                    else e::acc) [] (inner t)
    inner input |> Seq.filter (fun lst -> List.length lst = size)

let private linkedV (grid: string list) points = 
    Seq.forall (fun wall -> 
        let (x,y1) = Seq.head wall
        let y2 = Seq.last wall |> snd
        [y1 + 1..y2 - 1] |> Seq.forall (fun y -> Seq.contains grid.[y].[x] ['|';'+'])
    ) points

let private linkedH (grid: string list) points = 
    Seq.forall (fun floor -> 
        let (x1,y) = Seq.head floor
        let x2 = Seq.last floor |> fst
        [x1 + 1..x2 - 1] |> Seq.forall (fun x -> Seq.contains grid.[y].[x] ['-';'+'])
    ) points

let rectangles lines = 
    lines |> Seq.mapi (fun y line -> line |> Seq.mapi (fun x c -> if c = '+' then Some (x,y) else None))
          |> Seq.concat |> Seq.choose id |> Seq.toList |> subsets 4
          |> Seq.filter 
            (fun s -> 
                let walls = Seq.groupBy fst s |> Seq.map snd
                let floors = Seq.groupBy snd s |> Seq.map snd
                Seq.length walls = 2 
                && Seq.length floors = 2 
                && linkedV lines walls 
                && linkedH lines floors) |> Seq.length
