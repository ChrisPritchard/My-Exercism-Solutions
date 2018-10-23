module SpiralMatrix

type Direction = private | East | South | West | North
type Walker = private { x: int; y:int; direction: Direction }

let private turn direction = 
    match direction with
    | East -> South
    | South -> West
    | West -> North
    | North -> East

let private update x y value grid = 
    let row = Map.find y grid
    let newRow = List.take x row @ [value] @ List.skip (x + 1) row
    Map.add y newRow grid

let rec private step walker (grid:Map<int,int list>) = 
    let (x,y) =
        match walker.direction with
        | East -> (walker.x + 1, walker.y)
        | South -> (walker.x, walker.y + 1)
        | West -> (walker.x - 1, walker.y)
        | North -> (walker.x, walker.y - 1)
    let max = Map.count grid
    if x < 0 || y < 0 || x >= max || y >= max || grid.[y].[x] <> 0 then
        step { walker with direction = turn walker.direction } grid
    else
        { walker with x = x; y = y; }

let rec private walk walker grid remaining = 
    match remaining with
    | [] -> grid
    | [head] ->
        update walker.x walker.y head grid
    | head::tail ->
        let newGrid = update walker.x walker.y head grid
        let walker = step walker newGrid
        walk walker newGrid tail

let spiralMatrix size = 
    let all = [1..size*size]
    let grid = [0..size-1] |> List.map (fun i -> (i, [1..size] |> List.map (fun _ -> 0))) |> Map.ofList
    let walker = { x = 0; y = 0;direction = Direction.East }
    walk walker grid all |> Map.toList |> List.map (fun (_,row) -> row)