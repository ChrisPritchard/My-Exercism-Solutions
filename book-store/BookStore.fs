module BookStore

let total books = 
    let price num = float num * 8.0 * [1.0;0.95;0.9;0.8;0.75].[num - 1]
    let rec pricer cap set = seq {
        match set with
        | [] -> yield 0.0
        | _ ->
            let distinct = Seq.distinctBy (fun (_,n) -> n) set |> Seq.truncate cap
            yield distinct |> Seq.length |> price
            yield! Seq.except distinct set |> Seq.toList |> pricer cap }
    let marked = books |> List.mapi (fun i n -> i,n)
    [2..5] |> Seq.map (fun i -> pricer i marked |> Seq.sum) |> Seq.min
