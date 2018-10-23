module ErrorHandling
open System

let handleErrorByThrowingException() = failwith "Error"

let handleErrorByReturningOption (input:string) = 
    try
        int input |> Some
    with
    | _ -> None

let handleErrorByReturningResult input = 
    match handleErrorByReturningOption input with
    | Some n -> Ok n
    | None -> Error "Could not convert input to integer"

let bind switchFunction twoTrackInput = 
    match twoTrackInput with
    | Error s -> Error s
    | Ok s -> switchFunction s

let cleanupDisposablesWhenThrowingException (resource:IDisposable) = 
    try
        failwith "Error"
    finally
        resource.Dispose()