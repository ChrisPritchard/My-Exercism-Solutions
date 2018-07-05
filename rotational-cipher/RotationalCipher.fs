module RotationalCipher
open System

let rotate shiftKey text = 
    let rotateletter shiftKey c = 
        match int c + shiftKey with
        | r when Char.IsUpper c && r > int 'Z' -> r - 26 |> char
        | r when Char.IsLower c && r > int 'z' -> r - 26 |> char
        | r -> int r |> char
    let mapper (c:char) = if Char.IsLetter c then rotateletter shiftKey c else c
    text |> Seq.map mapper |> String.Concat
