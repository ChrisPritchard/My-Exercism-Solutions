module TwoBucket

type Bucket = | One = 1 | Two = 2
type Outcome = { Moves:int; GoalBucket:Bucket; OtherBucket:int }

let exchange (b1,b2) (_,m2) = 
    let nb2 = if b2 + b1 > m2 then m2 else b2 + b1
    let nb1 = if b2 + b1 <= m2 then 0 else b1 - (m2 - b2)
    (nb1, nb2)

let exchangeBack (b1,b2) (m1,_) = 
    let (r2, r1) = exchange (b2,b1) (0,m1)
    (r1, r2)

let rec test moves (m1,m2) target forbidden current : Outcome option =
    let newmoves = moves @ [current]
    match current with
    | (b1,b2) when b1 = target -> 
        Some { Moves = newmoves.Length; GoalBucket = Bucket.One; OtherBucket = b2 }
    | (b1,b2) when b2 = target -> 
        Some { Moves = newmoves.Length; GoalBucket = Bucket.Two; OtherBucket = b1 }
    | (b1,b2) ->
        [(b1,0);(0,b2);(m1,b2);(b1,m2);exchange (b1,b2) (m1,m2);exchangeBack (b1,b2) (m1,m2)]
        |> List.filter (fun o -> List.contains o newmoves |> not && o <> forbidden) 
        |> List.map (test newmoves (m1,m2) target forbidden)
        |> List.choose id |> List.sortBy (fun r -> r.Moves) |> List.tryHead

let measure size1 size2 target (start:Bucket) : Outcome =
    let buckets = if start = Bucket.One then (size1,0) else (0,size2)
    let forbidden = if start = Bucket.One then (0,size2) else (size1,0)
    test [] (size1, size2) target forbidden buckets
    |> function
    | Some r -> r
    | None -> failwith "Unsolvable"
