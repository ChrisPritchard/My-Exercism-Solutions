module NthPrime

let prime nth : int option = 
    let rec finder primes current = 
        if List.length primes >= nth then List.tryLast primes
        elif List.exists (fun n -> current % n = 0) primes then finder primes (current + 1)
        else finder (primes @ [current]) (current + 1)
    finder [] 2
