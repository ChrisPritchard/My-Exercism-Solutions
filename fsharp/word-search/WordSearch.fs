module WordSearch

let search (grid:string list) wordsToSearchFor = 
    let directions = [(-1,-1);(0,-1);(1,-1);(1,0);(1,1);(0,1);(-1,1);(-1,0);]

    let isValid (x,y) = x >= 0 && y >= 0 && x < grid.[0].Length && y < grid.Length

    let rec crawler (x,y) (dx,dy) chars =
        match chars with
        | [c] when grid.[y].[x] = c -> Some (x,y)
        | c::tail when grid.[y].[x] = c ->
            let next = (x + dx, y + dy)
            if isValid next then crawler next (dx,dy) tail
            else None
        | _ -> None

    let crawl (x,y) (word:string) = 
        let chars = word |> Seq.toList
        let found = directions |> List.map (fun dir -> crawler (x,y) dir chars) |> List.choose id
        match found with
        | [(x2,y2)] -> Some (word, Some ((x + 1, y + 1),(x2 + 1, y2 + 1)))
        | _ -> None

    let crawlAll (x,y) = wordsToSearchFor |> List.map (crawl (x,y)) |> List.choose id

    let result = [0..grid.Length-1] |> List.map (fun y -> [0..grid.[0].Length-1] |> List.map (fun x -> crawlAll (x,y)))
    let foundMap = result |> List.concat |> List.concat |> Map.ofList
    wordsToSearchFor |> List.fold (fun map word -> 
        if Map.containsKey word map |> not then Map.add word None map else map) foundMap
