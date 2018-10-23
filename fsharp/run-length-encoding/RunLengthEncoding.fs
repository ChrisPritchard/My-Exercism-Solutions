module RunLengthEncoding
open System

let encode input = 
    if input = "" then "" else
        let format (c, n) = (if n = 1 then "" else string n) + c.ToString()
        let counter (result, (counted, count)) c = 
            if c = counted 
            then (result, (counted, count + 1)) 
            else (result + format (counted, count), (c, 1))
        let state = Seq.fold counter ("", (input.[0], 1)) input.[1..]
        fst state + format (snd state)

let decode input = 
    let format (c:char) n = 
        if n = 0 then string c 
        else [0..n - 1] |> Seq.map (fun _ -> c) |> String.Concat
    let counter (result, count) c =
        if Char.IsNumber c 
        then (result, count * 10 + (string c |> int))
        else (result + format c count, 0)
    Seq.fold counter ("", 0) input |> fst
