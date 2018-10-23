module GoCounting

type Owner = | Black = 0 | White = 1 | None = 2

let map (board: string list) = 
    board |> Seq.indexed |> Seq.collect (fun (y, l) -> 
        l |> Seq.indexed |> Seq.map (fun (x, c) -> ((x,y),c)))
    |> Map.ofSeq

let findOwner tiles =
    tiles |> List.map snd |> List.except [' '] |> List.distinct 
        |> function
        | ['B'] -> Owner.Black
        | ['W'] -> Owner.White
        | _ -> Owner.None

let rec expand map path point =
    let c = Map.find point map
    match point with
    | p when c <> ' ' -> path @ [(p, c)]
    | (x,y) ->
        let newPath = path @ [(point, c)]
        let viable = 
            [(-1, 0);(1, 0);(0, -1);(0, 1)]
            |> List.map (fun (dx,dy) -> x + dx, y + dy)
            |> List.filter (fun p -> Map.containsKey p map && not (List.exists (fst >> (=) p) newPath))
        match viable with
        | [] -> newPath
        | _ -> viable |> List.collect (expand map newPath)

let extent (map: Map<(int * int), char> ) position =
    let tiles = expand map [] position |> List.distinct 
    let territory = tiles |> List.filter (snd >> (=) ' ') |> List.map fst |> List.sort
    (findOwner tiles, territory)

let territory board position = 
    let mapped = map board
    match mapped |> Map.tryFind position with
    | None -> None
    | Some c when c <> ' ' -> Some (Owner.None, [])
    | _ -> extent mapped position |> Some

let territories board =
    let mapped = map board
    mapped |> Map.toSeq |> Seq.map (fst >> extent mapped)
    |> Seq.append [(Owner.Black, []);(Owner.White, []);(Owner.None, [])] // ensures non-represented are part of final map
    |> Seq.groupBy fst |> Seq.map (fun (o,ps) -> 
        (o, ps |> Seq.collect snd |> Seq.distinct |> Seq.toList))
    |> Map.ofSeq