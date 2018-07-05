module Change

let findFewestCoins coins target = 
    let valid result = result |> List.sum |> (=) target
    let rec test sum nums result maxlen = 
        match nums with
        | _ when List.length result = maxlen -> result
        | [] -> result
        | h::t -> 
            if sum >= h then
                let subresult = test (sum - h) nums (result @ [h]) maxlen
                if not <| valid subresult then
                    test sum t result maxlen
                else
                    let otherresult = test sum t result (List.length subresult - 1)
                    if otherresult |> valid then otherresult else subresult
            else
                test sum t result maxlen
    let sorted = coins |> List.sortDescending
    let result = test target sorted [] target |> List.sort
    if result |> valid then Some result else None
