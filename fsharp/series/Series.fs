module Series

let slices (str:string) length = 
    if str.Length < length then invalidArg "length" "length cannot be greater than str length"
    else
        let intchar c = c.ToString() |> int
        let segment index = Seq.skip index str |> Seq.take length |> Seq.map intchar |> Seq.toList
        [0..str.Length - length] |> List.map segment |> Seq.toList