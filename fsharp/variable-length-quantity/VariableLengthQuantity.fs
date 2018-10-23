module VariableLengthQuantity

let rec asVLQ isFirst (number : uint32) =
    [
        if number > 127u then
            yield! asVLQ false (number >>> 7)
        
        let result = number &&& 127u

        if isFirst then yield byte result
        else yield byte <| result + 128u
    ]

let fromVLQ bytes =
    if List.last bytes > 127uy then None
    else
        bytes 
            |> List.fold (fun (results, current) byte ->
                let value = uint32 byte
                if value > 127u then
                    let newCurrent = (current <<< 7) + (value - 128u)
                    results, newCurrent
                else
                    let newResult = (current <<< 7) + value
                    (uint32 newResult)::results, 0u) ([], 0u)
            |> fst |> List.rev |> Some

let encode (numbers: uint32 list): byte list =
    numbers |> List.collect (asVLQ true)

let decode (bytes: byte list) =
    bytes |> fromVLQ