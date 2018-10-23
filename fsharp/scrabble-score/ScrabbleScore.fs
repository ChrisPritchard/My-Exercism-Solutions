module ScrabbleScore
open System

let scoreMapText = 
    @"A, E, I, O, U, L, N, R, S, T     1
    D, G                               2
    B, C, M, P                         3
    F, H, V, W, Y                      4
    K                                  5
    J, X                               8
    Q, Z                               10" 
let scoreMap = 
    let processRow row =
        let value = Array.head row |> int
        Array.tail row |> Array.map (fun c -> (c, value))
    let cells = 
        scoreMapText.Split('\n', StringSplitOptions.RemoveEmptyEntries) 
        |> Array.map (fun line -> line.Replace(",", "").Split(' ', StringSplitOptions.RemoveEmptyEntries) |> Array.rev |> processRow)
    cells |> Array.collect id |> Map.ofArray

let score (word:string) = Seq.sumBy (fun x -> scoreMap.[x.ToString().ToUpper()]) word
