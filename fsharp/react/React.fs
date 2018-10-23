module React

[<AbstractClass>]
type Cell() =
    abstract Value: int with get, set
    abstract Changed: IEvent<int>

type InputCell (initialValue: int) =
    inherit Cell()

    let mutable value = initialValue
    let changed = new Event<int>()
    override __.Changed = changed.Publish

    override __.Value
        with get() = value
        and set(newValue) = 
            if newValue <> value then 
                value <- newValue
                changed.Trigger newValue
                
type ComputeCell (sources: Cell list, computation: int list -> int) =
    inherit Cell()

    let compute () = computation (sources |> List.map (fun o -> o.Value))
    let mutable oldValue = compute ()
    let changed = new Event<int>()

    let update () = 
        let newValue = compute()
        if newValue <> oldValue then 
            oldValue <- newValue
            changed.Trigger newValue
    do
        sources |> List.iter (fun o -> o.Changed.Add (fun _ -> update ()))

    override __.Changed = changed.Publish

    override __.Value
        with get() = compute()
        and set(_) = failwith "Compute Cells cant be set"

type Reactor() =
    member __.createInputCell initialValue = new InputCell (initialValue)
    member __.createComputeCell sources computation = new ComputeCell (sources, computation)