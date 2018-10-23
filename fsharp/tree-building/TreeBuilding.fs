module TreeBuilding

type Record = { RecordId: int; ParentId: int }
type Tree = 
    | Branch of int * Tree list
    | Leaf of int

let recordId t = 
    match t with
    | Branch (id, c) -> id
    | Leaf id -> id

let isBranch t = 
    match t with
    | Branch (id, c) -> true
    | Leaf id -> false

let children t = 
    match t with
    | Branch (id, c) -> c
    | Leaf id -> []

let rec buildFrom remaining current = 
    let partitioned = remaining |> List.partition (fun x -> x.ParentId = current.RecordId) 
    match partitioned with
    | ([],_) -> Leaf current.RecordId
    | (children,remaining) -> Branch (current.RecordId, children |> List.map (buildFrom remaining) |> List.sort)

let buildTree records = 
    if List.isEmpty records then failwith "Empty input"
    if List.exists (fun x -> x.RecordId < x.ParentId) records then failwith "Invalid parent relationship"
    if records |> List.map (fun x -> x.RecordId) |> List.max <> records.Length - 1 then failwith "Non continuous"

    let root = List.partition (fun x -> x.RecordId = 0) records
    if List.exists (fun x -> x.RecordId = x.ParentId) (snd root) then failwith "Invalid cycle"

    match root with 
    | ([],_) -> failwith "No root found"
    | (r,_) when r.Length > 1 -> failwith "More than one root"
    | (r,_) when r.[0].ParentId <> 0 -> failwith "Root is invalid"
    | (r,remaining) -> buildFrom remaining r.[0]
