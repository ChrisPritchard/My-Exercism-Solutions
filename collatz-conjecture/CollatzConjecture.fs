module CollatzConjecture

let rec stepsRec (x, length) =
    match x with
    | 1 -> Some length
    | _ when x % 2 = 0 -> stepsRec (x / 2, length + 1) 
    | _ -> stepsRec (x * 3 + 1, length + 1)

let steps (number: int): int option =
    if(number <= 0) then None else stepsRec (number, 0)
