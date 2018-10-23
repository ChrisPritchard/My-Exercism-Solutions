module Connect

type Players = | Black | White

let private playedTiles board =
    board 
        |> Seq.mapi (fun y line -> line |> Seq.mapi (fun x c -> (c, (x, y))))
        |> Seq.collect id |> Seq.filter (fun (c, _) -> c = 'X' || c = 'O')
        |> Seq.groupBy fst |> Seq.map (fun (c, sp) -> 
            (c, sp |> Seq.map snd |> Seq.toList))
        |> Map.ofSeq

let private options playerTiles moves (x, y) =
    [(-1,-1);(1,-1);(2,0);(1,1);(-1,1);(-2,0)]
    |> List.map (fun (dx, dy) -> (x + dx, y + dy))
    |> List.filter (fun p -> List.contains p playerTiles && not (List.contains p moves))

let private findRoute startOptions winCondition playerTiles = 
    let rec crawler moves winCondition point = 
        if winCondition point then true else
            match options playerTiles moves point with
            | [] -> false
            | o -> 
                List.exists id (List.map (crawler (moves @ [point]) winCondition) o)
    List.exists id (List.map (crawler [] winCondition) startOptions)

let winner (board: string list) =
    let marked = playedTiles board 
    let whiteGame = 
        marked.ContainsKey 'O'
        && findRoute 
            (List.filter (fun (_, y) -> y = 0) marked.['O'])
            (fun (_, y) -> y = board.Length - 1)
            marked.['O']
    if whiteGame then Some White 
    else
        let boardWidth = if board.Length = 0 then 0 else board.[0].Length
        let blackGame = 
            marked.ContainsKey 'X'
            && findRoute 
                (List.filter (fun (x, y) -> x = y) marked.['X'])
                (fun (x, y) -> x = boardWidth - (board.Length - y))
                marked.['X']
        if blackGame then Some Black else None
