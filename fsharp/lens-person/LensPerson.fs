module LensPerson

open System
open Aether.Operators

type Person = {
    name: NameInfo;
    born: BirthInfo;
    address: AddressInfo

} with 
    static member bornLens = (fun p -> p.born), (fun born p -> { p with born = born })
    static member addressLens = (fun p -> p.address), (fun address p -> { p with address = address })
and NameInfo = {
    name: string; surName: string
} and BirthInfo = {
    at: AddressInfo;
    on: DateTime
} with 
    static member atLens = (fun b -> b.at), (fun at b -> { b with at = at })
    static member onLens = (fun b -> b.on), (fun on b -> { b with on = on })
and AddressInfo = {
    street: string;
    houseNumber: int;
    place: string;
    country: string
} with 
    static member streetLens = (fun a -> a.street), (fun street a -> { a with street = street })

type DateTime with
    static member monthNumLens = (fun (d:DateTime) -> d.Month), (fun month (d:DateTime) -> new DateTime (d.Year, month, d.Day))

let bornAtStreet = Person.bornLens >-> BirthInfo.atLens >-> AddressInfo.streetLens

let currentStreet = Person.addressLens >-> AddressInfo.streetLens

let bornOn = Person.bornLens >-> BirthInfo.onLens

let birthMonth = bornOn >-> DateTime.monthNumLens
