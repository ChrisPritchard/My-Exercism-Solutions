module Grains

let square (n: int): Result<uint64,string> = 
    match n with
    | _ when n <= 0 || n > 64 -> Error "Invalid input"
    | _ -> pown 2UL (n - 1) |> Ok

let total: Result<uint64,string> = [1..64] |> Seq.sumBy (fun x -> pown 2UL (x - 1)) |> Ok
