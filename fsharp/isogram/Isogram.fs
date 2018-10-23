module Isogram

let isIsogram (str:string) = 
    let clean = str.ToLowerInvariant() |> Seq.filter System.Char.IsLetter |> Seq.toArray
    clean |> Seq.distinct |> Seq.length |> (=) clean.Length