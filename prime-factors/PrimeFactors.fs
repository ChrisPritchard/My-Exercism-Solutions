module PrimeFactors

let factors number = 
    let unfolder = function
        | (1L, _) -> None
        | (n, r) when n % r = 0L -> Some (int r |> Some, (n / r, r))
        | (n, r) -> Some (None, (n, r + 1L))
    List.unfold unfolder (number, 2L) |> List.choose id
