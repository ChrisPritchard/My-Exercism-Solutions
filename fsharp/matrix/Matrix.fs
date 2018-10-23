module Matrix

let rowsFrom (matrix:string) = matrix.Split('\n') |> Array.map (fun (r:string) -> r.Split(' ') |> Seq.map int |> Seq.toList)

let row index matrix = (rowsFrom matrix).[index]

let column index matrix = rowsFrom matrix |> Array.map (fun r -> r.[index]) |> Array.toList