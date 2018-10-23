module Tournament

let tally input = 
    let update team res teams =
        let (mp,w,d,l,p) = match Map.tryFind team teams with | Some n -> n | None -> (0,0,0,0,0)
        let newpoints = 
            match res with
            | "win" -> (mp + 1,w + 1,d,l,p + 3)
            | "draw" -> (mp + 1,w,d + 1,l,p + 1)
            | _ -> (mp + 1,w,d,l + 1,p)
        teams.Add (team, newpoints)
    let rev res = match res with | "win" -> "loss" | "loss" -> "win" | _ -> res

    let result = 
        input 
        |> List.map (fun (l:string) -> l.Split(';') |> fun l -> (l.[0],l.[1],l.[2]))
        |> List.fold 
            (fun teams (t1, t2, res) -> 
                let teams2 = update t1 res teams
                update t2 (rev res) teams2) Map.empty<string, (int * int * int * int * int)>
        |> Map.toList 
        |> List.sortBy (fun (team,(_,_,_,_,_)) -> [team]) |> List.sortByDescending (fun (_,(_,_,_,_,p)) -> [p])

    [sprintf "%-31s| MP |  W |  D |  L |  P" "Team"] 
        @ (result |> List.map (fun (team,(mp,w,l,d,p)) -> sprintf "%-31s| %2i | %2i | %2i | %2i | %2i" team mp w l d p))
