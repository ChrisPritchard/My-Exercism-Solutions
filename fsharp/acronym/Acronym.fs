module Acronym
open System

let abbreviate (phrase:string) = phrase.Split([|' ';'-'|]) |> Seq.map (Seq.head >> Char.ToUpper) |> String.Concat