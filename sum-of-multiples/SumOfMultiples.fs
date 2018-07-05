module SumOfMultiples

let sum (numbers: int list) (upperBound: int) =
    seq { for n in 1..(upperBound - 1)
        do if List.exists (fun x -> n % x = 0) numbers then 
            yield n } |> Seq.sum
