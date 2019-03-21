module DndCharacter

open System

let random = Random ()

let modifier x =
    float (x - 10) / 2. |> floor |> int

let ability() = 
    List.init 4 (fun _ -> random.Next(6) + 1)
    |> List.skip 1 
    |> List.sum

type DndCharacter() =

    let str, dex, con, int, wis, chr =
        ability (), ability (), ability (), 
        ability (), ability (), ability ()

    member __.Strength with get() = str
    member __.Dexterity with get() = dex
    member __.Constitution with get() = con
    member __.Intelligence with get() = int
    member __.Wisdom with get() = wis
    member __.Charisma with get() = chr
    member __.Hitpoints with get() = modifier con + 10

