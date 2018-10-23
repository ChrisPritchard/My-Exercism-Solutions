module Sieve

let primes limit = 
    let max = float limit |> sqrt |> floor |> int
    let sieve excluded current = 
        if List.contains current excluded 
            then excluded 
            else excluded @ [2*current..current..limit]
    let excluded = [2..max] |> Seq.fold sieve []
    Seq.except excluded [2..limit] |> Seq.toList