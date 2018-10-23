module BinarySearch

let find input value =
    let sorted = Array.sort input
    let rec search input indexOffset value = 
        match input with 
        | [||] -> None
        | _ -> 
            let mid = input.Length / 2
            match input.[mid] with
            | x when x > value -> 
                let right = Array.take mid input
                search right indexOffset value
            | x when x < value -> 
                let left = Array.skip (mid + 1) input
                search left (indexOffset + mid + 1) value
            | _ -> indexOffset + mid |> Some
    search sorted 0 value
