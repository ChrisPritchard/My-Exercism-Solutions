module CustomSet

type CustomSet (data: int seq) =
    member this.data = data |> Seq.distinct |> Seq.sort |> Seq.toList

let empty = new CustomSet([])

let isEmpty (set:CustomSet) = List.isEmpty set.data

let size (set:CustomSet) = List.length set.data

let fromList list = new CustomSet(list)
let toList (set:CustomSet) = set.data

let contains value (set:CustomSet) = List.contains value set.data

let insert value (set:CustomSet) = new CustomSet([value] @ set.data)

let isEqualTo (left:CustomSet) (right:CustomSet) = left.data = right.data

let union (left:CustomSet) (right:CustomSet) = new CustomSet(left.data @ right.data)

let intersection (left:CustomSet) (right:CustomSet) = 
    let isLeft = right.data |> List.filter (fun x -> List.contains x left.data)
    let isRight = left.data |> List.filter (fun x -> List.contains x right.data)
    new CustomSet(isLeft @ isRight)

let difference (left:CustomSet) (right:CustomSet) = 
    let notRight = left.data |> List.filter (fun x -> not <| List.contains x right.data)
    new CustomSet(notRight)

let isSubsetOf (left:CustomSet) (right:CustomSet) = 
    match left.data, right.data with
    | ([],_) -> true
    | (_,[]) -> false
    | _ ->
        right.data |> List.chunkBySize left.data.Length |> List.exists ((=) left.data)

let isDisjointFrom (left:CustomSet) (right:CustomSet) = 
    match left.data, right.data with
    | ([],_) -> true
    | (_,[]) -> true
    | _ ->
        let allRight = not <| List.exists (fun x -> List.contains x left.data) right.data
        let allLeft = not <| List.exists (fun x -> List.contains x right.data) left.data
        allRight && allLeft
