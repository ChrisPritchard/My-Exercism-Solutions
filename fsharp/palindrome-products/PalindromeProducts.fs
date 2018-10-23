module PalindromeProducts

let private isPalindrome num = 
    let rec reverse (num,res) =
        match num with
        | 0 -> res
        | _ -> reverse (num/10,res * 10 + num % 10)
    reverse (num, 0) |> (=) num

let private palindromes minFactor maxFactor = 
    Seq.collect (fun a -> [minFactor..a] |> Seq.map (fun b -> a*b)) [minFactor..maxFactor]
    |> Seq.filter isPalindrome

let private factorise minFactor maxFactor num =
    let factors = 
        [minFactor..maxFactor] |> Seq.map (fun a -> (a, num / a, num % a))
        |> Seq.filter (fun (_,b,m) -> m = 0 && b >= minFactor && b <= maxFactor)
        |> Seq.map (fun (a,b,_) -> if a < b then (a,b) else (b,a)) |> Seq.distinct |> Seq.toList
    (num, factors)

let largest minFactor maxFactor = 
    let set = palindromes minFactor maxFactor
    if Seq.isEmpty set then None else set |> Seq.max |> factorise minFactor maxFactor |> Some

let smallest minFactor maxFactor = 
    let set = palindromes minFactor maxFactor
    if Seq.isEmpty set then None else set |> Seq.min |> factorise minFactor maxFactor |> Some