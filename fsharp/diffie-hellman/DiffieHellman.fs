module DiffieHellman

let privateKey (primeP: bigint) = 
    let random = new System.Random()
    random.NextDouble() * (primeP - 1I |> float) |> ceil |> bigint

let publicKey primeP primeG privateKey = bigint.ModPow (primeG, privateKey, primeP)

let secret primeP publicKey privateKey = bigint.ModPow (publicKey, privateKey, primeP)