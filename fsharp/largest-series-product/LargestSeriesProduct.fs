module LargestSeriesProduct
open System

let largestProduct (input:string) seriesLength: int option = 
    match input with
    | "" when seriesLength > 0 -> None
    | _ when seriesLength < 0 -> None
    | _ when seriesLength > input.Length -> None
    | _ when Seq.exists (Char.IsDigit >> not) input -> None
    | _ when seriesLength = 0 -> Some 1
    | _ ->
        let digits = input |> Seq.map (string >> int) |> Seq.toArray
        digits |> Seq.windowed seriesLength |> Seq.map (Seq.fold (*) 1) |> Seq.max |> Some