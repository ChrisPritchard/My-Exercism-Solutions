module SimpleLinkedList

type ListItem = { data: int; next: ListItem Option }

let nil = None

let create x n = Some { data = x; next = n }

let isNil x = match x with | None -> true | _ -> false

let next (x:ListItem option) = match x with | None -> nil | Some i -> i.next


let datum (x:ListItem option) = match x with | None -> 0 | Some i -> i.data

let rec toList (x: ListItem Option) = 
    match x with
    | None -> []
    | Some x -> 
        seq { 
            yield x.data
            yield! toList x.next
        } |> Seq.toList

let fromList xs = xs |> Seq.rev |> Seq.fold (fun acc x -> create x acc) nil

let reverse x = x |> toList |> Seq.rev |> fromList