module RestApi
open Newtonsoft.Json

type Database = {
    users: User []
}
and User = {
    name: string
    owes: Map<string, float>
    owed_by: Map<string, float>
    balance: float
}

type UserRequest = {
    users: string []
}
type AddRequest = {
    user: string
}
type ChangeRequest = {
    lender: string
    borrower: string
    amount: float
}

let parse<'T> = JsonConvert.DeserializeObject<'T>
let json = JsonConvert.SerializeObject
let notFound () = failwith "url not recognised (404)"

let private updateBalance user =
    let newBalance = 
        (user.owed_by |> Map.toList |> List.sumBy snd)
        - (user.owes |> Map.toList |> List.sumBy snd)
    { user with balance = newBalance }

let private applyChange lender borrower amount =
    let currentOwedByLender = Map.tryFind borrower.name lender.owes |> Option.defaultValue 0.
    if currentOwedByLender = 0. then
        let newLender = { lender with owed_by = Map.add borrower.name amount lender.owed_by }
        let newBorrower = { borrower with owes = Map.add lender.name amount borrower.owes }
        updateBalance newLender, updateBalance newBorrower
    elif currentOwedByLender > amount then
        let newLender = { lender with owes = Map.add borrower.name (currentOwedByLender - amount) lender.owes }
        let newBorrower = { borrower with owed_by = Map.add lender.name (currentOwedByLender - amount) borrower.owed_by }
        updateBalance newLender, updateBalance newBorrower
    elif currentOwedByLender = amount then
        let newLender = { lender with owes = Map.remove borrower.name lender.owes }
        let newBorrower = { borrower with owed_by = Map.remove lender.name borrower.owes }
        updateBalance newLender, updateBalance newBorrower
    else
        let newLender = 
            { lender with 
                owed_by = Map.add borrower.name (amount - currentOwedByLender) lender.owed_by
                owes = Map.remove borrower.name lender.owes }
        let newBorrower = 
            { borrower with 
                owes = Map.add lender.name (amount - currentOwedByLender) borrower.owes
                owed_by = Map.remove lender.name lender.owed_by }
        updateBalance newLender, updateBalance newBorrower

let private updateOwed amount otherName user =
    let current = Map.tryFind otherName user.owed_by |> Option.defaultValue 0.
    let newOwed = 
        Map.add otherName (current - amount) user.owed_by
        |> Map.filter (fun _ value -> value > 0.)
    { user with owed_by = newOwed }


type RestApi(database : string) =

    let mutable database = parse database

    member __.Get(url: string) =
        match url with
        | "/users" -> json database
        | _ -> notFound ()

    member __.Get(url: string, payload: string) =
        match url with
        | "/users" ->
            let request = parse<UserRequest> payload
            let filtered: Database = 
                { database with 
                    users = 
                        database.users 
                        |> Array.filter (fun u -> 
                            Array.contains u.name request.users)
                        |> Array.sortBy (fun u -> u.name) }
            json filtered
        | _ -> notFound ()

    member __.Post(url: string, payload: string)  =
        match url with
        | "/add" ->
            let request = parse<AddRequest> payload
            let newUser = {
                name = request.user
                owes = Map.empty
                owed_by = Map.empty
                balance = 0.
            }
            database <- { database with users = Array.append [|newUser|] database.users }
            json newUser
        | "/iou" ->
            let request = parse<ChangeRequest> payload
            let lender = Array.find (fun u -> u.name = request.lender) database.users
            let borrower = Array.find (fun u -> u.name = request.borrower) database.users
            let newLender, newBorrower = applyChange lender borrower request.amount

            let newUsers = 
                database.users 
                |> Array.map (fun u -> 
                    if u.name = lender.name then newLender 
                    elif u.name = borrower.name then newBorrower 
                    else u)
            database <- { database with users = newUsers }

            let result: Database = 
                { users = Array.sortBy (fun u -> u.name) [|newLender;newBorrower|]}
            json result
        | _ -> notFound ()