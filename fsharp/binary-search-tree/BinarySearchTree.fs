module BinarySearchTree

type Node = { data: int; left: Node Option; right: Node Option }

let left node  = node.left
let right node = node.right
let data node = node.data

let sortedData node =
    let rec sort node = 
        match node with
        | None -> []
        | Some value -> List.concat [sort value.left;[value.data];sort value.right]
    node |> Some |> sort

let create items =
    let rec createNode items = 
        match items with
        | [] -> None
        | list -> 
            let data = List.head list
            let branches = List.tail list |> List.partition (fun x -> x <= data)
            Some { data = data; left = fst branches |> createNode; right = snd branches |> createNode }
    match createNode items with | None -> failwith "Bad data" | Some node -> node
