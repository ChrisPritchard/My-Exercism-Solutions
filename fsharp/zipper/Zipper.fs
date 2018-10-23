module Zipper

type Node = private { Value: int; Left: Node option; Right: Node option }
type Nav = private | Left | Right
type Zipper = private { Parents: (Node * Nav) list; Focus: Node }

let tree value left right = { Value = value; Left = left; Right = right }

let left zipper = 
    match zipper.Focus.Left with
    | None -> None
    | Some l -> Some { Focus = l; Parents = (zipper.Focus, Nav.Left)::zipper.Parents }
let right zipper = 
    match zipper.Focus.Right with
    | None -> None
    | Some r -> Some { Focus = r; Parents = (zipper.Focus, Nav.Right)::zipper.Parents }
let up zipper = 
    match zipper.Parents with 
    | [] -> None
    | h::tail -> 
        let newFocus = 
            if snd h = Nav.Left 
            then { fst h with Left = Some zipper.Focus }
            else { fst h with Right = Some zipper.Focus }
        Some { Focus = newFocus; Parents = tail }

let setValue value zipper = { zipper with Focus = { zipper.Focus with Value = value } }
let setLeft node zipper = { zipper with Focus = { zipper.Focus with Left = node } }
let setRight node zipper = { zipper with Focus = { zipper.Focus with Right = node } }

let fromTree tree = { Parents = []; Focus = tree }
let rec toTree zipper = 
    match up zipper with 
    | None -> zipper.Focus 
    | Some parent -> toTree parent
let value zipper = zipper.Focus.Value
