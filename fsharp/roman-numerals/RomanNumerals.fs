module RomanNumerals

let roman arabicNumeral =
    let digits = sprintf "%04i" arabicNumeral |> Seq.map (fun c -> int <| c.ToString()) |> Seq.toList
    let concat (l:string list) = System.String.Concat l
    let get symbols n =
        let (n1,n5,n10) = symbols
        match digits.[n] with
        | 1 -> n1
        | 2 -> concat [n1;n1]
        | 3 -> concat [n1;n1;n1]
        | 4 -> concat [n1;n5]
        | 5 -> n5
        | 6 -> concat [n5;n1]
        | 7 -> concat [n5;n1;n1]
        | 8 -> concat [n5;n1;n1;n1]
        | 9 -> concat [n1;n10]
        | _ -> ""
    concat [
        get ("M","","") 0;
        get ("C","D","M") 1;
        get ("X","L","C") 2;   
        get ("I","V","X") 3
    ]