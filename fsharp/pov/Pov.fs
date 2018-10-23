module Pov

type Graph<'a> = { value: 'a; children: Graph<'a> list }

let mkGraph value children = { value = value; children = children }

let rec private find byValue parents current =
        if current.value = byValue then Some (current, parents)
        elif List.isEmpty current.children then None
        else 
            let newParents = current::parents
            List.map (find byValue newParents) current.children 
            |> List.tryPick id

let rec private reseat parent chain current =
    let newElem = 
        { current with 
            children = current.children |> List.filter (fun o -> o.value <> parent.value) }
    match chain with
    | [] -> newElem
    | p::prest -> 
        { newElem with 
            children = newElem.children @ [reseat newElem prest p]}

let fromPOV byValue graph =
    match find byValue [] graph with
    | None -> None
    | Some (elem, []) -> Some elem
    | Some (elem, p::prest) ->
        Some { elem with children = elem.children @ [reseat elem prest p]}

let private findCommon path1 (path2: 'a list) =
    let common = 
        path1
        |> List.indexed 
        |> List.filter (fun (i,o) -> o = path2.[i])
        |> List.map snd
    let start = path1 |> List.except common |> List.rev
    start @ [List.last common] @ (List.except common path2)

let private getPath value graph =
    find value [] graph 
    |> Option.bind (fun p -> [fst p] @ snd p |> List.map (fun o -> o.value) |> List.rev |> Some)

let tracePathBetween value1 value2 graph =
    match getPath value1 graph with
    | None -> None
    | Some p1 ->
        if List.contains value2 p1 
        then List.skipWhile (fun o -> o <> value2) p1 |> List.rev |> Some
        else 
            match getPath value2 graph with
            | None -> None
            | Some p2 -> 
                if List.contains value1 p2 
                then List.skipWhile (fun o -> o <> value1) p2 |> List.rev |> Some
                else findCommon p1 p2 |> Some