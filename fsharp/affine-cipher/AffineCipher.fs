module AffineCipher

open System

let m = 26

let coprime a b =
    let mutable p1, p2 = a, b
    while p1 <> 0 && p2 <> 0 do
        if p1 > p2 then p1 <- p1 % p2
        else p2 <- p2 % p1
    (max p1 p2) = 1

let clean (s: string) = 
    s.ToLower() |> Seq.filter Char.IsLetterOrDigit

let decode a b cipheredText = 
    if not (coprime a m) then
        invalidArg "a" (sprintf "%i is not coprime with %i" a m)

    let modInverse = [1..m] |> List.find (fun i -> i * a % m = 1) 
    let decode (c: char) = 
        if Char.IsDigit c then c
        else
            let v = (modInverse * ((int c - int 'a') - b)) % m
            let vc = if v < 0 then v + m else v
            char (vc + int 'a')

    clean cipheredText
    |> Seq.map decode 
    |> Seq.toArray
    |> String

let encode a b plainText = 
    if not (coprime a m) then
        invalidArg "a" (sprintf "%i is not coprime with %i" a m)
    
    let encode (c: char) =
        if Char.IsDigit c then c
        else
            let v = ((int c - int 'a') * a + b) % m
            char (v + int 'a')

    clean plainText
    |> Seq.map encode 
    |> Seq.toArray
    |> Array.chunkBySize 5 
    |> Array.map String 
    |> String.concat " "
