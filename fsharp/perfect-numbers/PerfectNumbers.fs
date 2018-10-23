module PerfectNumbers

type Classification = Perfect | Abundant | Deficient 

let classify n : Classification option = 
    if n <= 0 then None 
    else
        let factorSum = Seq.fold (fun acc x -> if n % x = 0 then acc + x else acc) 0 [1..n-1]
        match factorSum with
        | _ when factorSum < n -> Some Deficient
        | _ when factorSum > n -> Some Abundant
        | _ -> Some Perfect