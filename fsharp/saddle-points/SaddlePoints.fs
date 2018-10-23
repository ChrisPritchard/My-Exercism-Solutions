module SaddlePoints

let saddlePoints (matrix: int list list) = 
    match matrix with
    | [] -> []
    | [[]] -> []
    | _ -> 
        let maxPerRow = matrix |> List.map List.max
        let indexed = matrix |> List.mapi (fun ri r -> r |> List.mapi (fun ci c -> (c, ri, ci))) |> List.collect id
        seq { 
            for (v,r,c) in indexed do 
                if v = maxPerRow.[r] && List.forall (fun row -> v <= matrix.[row].[c]) [0..matrix.Length-1] 
                    then yield (r, c)
            } |> Seq.toList