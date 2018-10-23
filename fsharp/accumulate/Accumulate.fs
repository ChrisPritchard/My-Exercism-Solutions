module Accumulate

let accumulate<'a, 'b> (func: 'a -> 'b) (input: 'a list): 'b list = 
    seq { for x in input do yield func x } |> Seq.toList
