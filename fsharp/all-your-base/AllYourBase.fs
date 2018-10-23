module AllYourBase

let rebase digits inputBase outputBase = 
    match digits with
    | _ when inputBase < 2 || outputBase < 2 -> None
    | _ when List.exists (fun x -> x >= inputBase || x < 0) digits -> None
    | [] -> Some [0]
    | _ ->
        let base10 = Seq.rev digits |> Seq.mapi (fun i x -> pown inputBase i |> (*) x) |> Seq.sum
        let rec convert x = 
            seq { 
                yield x % outputBase
                let quotient = float x / float outputBase |> floor |> int
                match quotient with | 0 -> () | _ -> yield! convert quotient
            }
        convert base10 |> Seq.rev |> Seq.toList |> Some