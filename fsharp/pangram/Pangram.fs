module Pangram

let isPangram (input: string): bool = 
    let lower = input.ToLowerInvariant()
    ['a'..'z'] |> Seq.forall (fun c -> Seq.contains c lower)