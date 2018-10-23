module SecretHandshake

let commands number = 
    let set = [(1,"wink");(2,"double blink");(4,"close your eyes");(8,"jump")]
    let deciphered = set |> List.filter (fun x -> fst x &&& number = fst x) |> List.map snd
    if (16 &&& number = 16) then List.rev deciphered else deciphered