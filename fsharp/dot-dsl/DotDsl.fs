module DotDsl

type Attr = { key:string;value:string }
type Node = { key:string;attrs:(string * string) list}
type Edge = { left:string;right:string;attrs:(string * string) list}

type GraphComponent =
    | Node of Node
    | Edge of Edge
    | Attribute of Attr

type Graph = { children:GraphComponent list }

let graph children = { children = children |> List.sortBy (function | Node n -> n.key | Attribute a -> a.key | Edge e -> e.left) }

let attr key value = Attribute { key = key;value = value }

let node key attrs = Node { key = key; attrs = attrs }

let edge left right attrs = Edge { left = left;right = right;attrs = attrs }

let attrs graph = List.choose (function | Attribute a -> Some <| Attribute a | _ -> None) graph.children

let nodes graph = List.choose (function | Node n -> Some <| Node n | _ -> None) graph.children 

let edges graph = List.choose (function | Edge e -> Some <| Edge e | _ -> None) graph.children