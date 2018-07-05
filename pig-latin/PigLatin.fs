module PigLatin

let translate (input:string) =
    let concat (c:char list) = System.String.Concat(c)
    let skip n cs = List.skip n cs |> concat
    let first n cs = List.take n cs

    let (|Vowel|Consonant|) (c:char) = if ['a';'e';'i';'o';'u'] |> List.contains c then Vowel else Consonant

    let translate (word:string) = 
        let chars = word.ToCharArray() |> Array.toList
        match chars.[0] with
        | Vowel -> word + "ay"
        | _ ->
            let s = chars @ [' ';' '] |> List.take 3
            match s with
            | ['q';'u';_] -> skip 2 chars + "quay"
            | [_;'q';'u';] -> skip 3 chars + s.[0].ToString() + "quay"
            | ['t';'h';'r';] -> skip 3 chars + "thray"
            | ['t';'h';_] -> skip 2 chars + "thay"
            | ['c';'h';_] -> skip 2 chars + "chay"
            | ['s';'c';'h';] -> skip 3 chars + "schay"
            | ['x';'r';_] -> word + "ay"
            | ['y';'t';_] -> word + "ay"
            | [_;_;'y'] -> skip 2 chars + (first 2 chars |> concat) + "ay"
            | _ -> skip 1 chars + s.[0].ToString() + "ay"
    let words = input.Split([|' '|]) |> Array.map translate
    System.String.Join(' ', words)
