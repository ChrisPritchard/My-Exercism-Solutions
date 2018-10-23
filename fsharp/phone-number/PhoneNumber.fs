module PhoneNumber

let clean input = 
    let valid x = x <> '0' && x <> '1'
    input |> Seq.filter System.Char.IsDigit 
    |> fun x -> if Seq.head x = '1' then Seq.tail x else x
    |> System.String.Concat
    |> fun x -> if x.Length = 10 && valid x.[0] && valid x.[3] then Some x else None