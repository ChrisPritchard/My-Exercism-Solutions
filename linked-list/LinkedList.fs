module LinkedList

type Node = {
    data: int
    mutable prev: Node Option
    mutable next: Node Option
}

type LinkedList = { 
    mutable root: Node Option
}

let mkLinkedList () = { root = None }

let pop linkedList = 
    match linkedList.root with
    | None -> 0
    | Some n ->
        let mutable current = n
        while current.next <> None do current <- current.next.Value
        if current.prev <> None then current.prev.Value.next <- None else linkedList.root <- None
        current.data

let shift linkedList = 
    match linkedList.root with
    | None -> 0
    | Some n ->
        if n.next <> None then 
            n.next.Value.prev <- None 
            linkedList.root <- n.next
        else linkedList.root <- None
        n.data

let push newValue linkedList = 
    match linkedList.root with
    | None -> linkedList.root <- Some { data = newValue; prev = None; next = None }
    | Some n ->
        let mutable current = n
        while current.next <> None do current <- current.next.Value
        current.next <- Some { data = newValue; prev = Some current; next = None }

let unshift newValue linkedList = 
    match linkedList.root with
    | None -> linkedList.root <- Some { data = newValue; next = None; prev = None }
    | Some n ->
        n.prev <- Some { data = newValue; next = Some n; prev = None }
        linkedList.root <- n.prev
