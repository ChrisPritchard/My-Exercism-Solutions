module PascalsTriangle

let rows numberOfRows : int list list option = 
    match numberOfRows with
    | _ when numberOfRows < 0 -> None
    | 0 -> Some []
    | _ ->
        let rec derive prevrow current = 
            seq { 
                    let newRow = 
                        match prevrow with 
                        | [] -> [1]
                        | _ -> [0] @ prevrow @ [0] |> List.windowed 2 |> List.map List.sum
                    yield newRow
                    match current with 
                    | 1 -> () 
                    | _ -> yield! derive newRow (current - 1)
            } |> Seq.toList
        derive [] numberOfRows |> Some