module DifferenceOfSquares

let squareOfSum (number: int): int = pown (List.sum [1..number]) 2

let sumOfSquares (number: int): int = List.sumBy (fun x -> pown x 2) [1..number]

let differenceOfSquares (number: int): int = (squareOfSum number) - (sumOfSquares number) |> abs