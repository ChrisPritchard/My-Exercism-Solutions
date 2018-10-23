module Forth
open System

let rec evaluate (input:string list): int list option = 
    let ops = 
        [
            ("+", function | (h1::h2::tail) -> (h2 + h1)::tail |> Some | _ -> None);
            ("/", function | (h1::h2::tail) when h1 <> 0 -> (h2 / h1)::tail |> Some | _ -> None);
            ("-", function | (h1::h2::tail) -> (h2 - h1)::tail |> Some | _ -> None);
            ("*", function | (h1::h2::tail) -> (h2 * h1)::tail |> Some | _ -> None);
            ("DUP", function | (h::tail) -> h::h::tail |> Some | _ -> None);
            ("DROP", function | (_::tail) -> tail |> Some | _ -> None);
            ("SWAP", function | (h1::h2::tail) -> h2::h1::tail |> Some | _ -> None);
            ("OVER", function | (h1::h2::tail) -> h2::h1::h2::tail |> Some | _ -> None);
        ] |> Map.ofList

    let instructions = 
        List.fold (fun (current: string) (userOp: string) -> 
            userOp
                .Trim([|':';';'|]).ToUpper()
                .Split([|' '|], StringSplitOptions.RemoveEmptyEntries)
            |> fun a -> 
                current.Replace(Array.head a, String.concat " " (Array.tail a)))
            ((List.last input).ToUpper()) (List.rev input |> List.tail)

    instructions.Split([|' '|])
    |> Array.toList
    |> List.fold (fun stack o ->
        match stack with
        | None -> None
        | Some s ->
            if ops.ContainsKey(o) then ops.[o] s
            elif Seq.exists (Char.IsDigit >> not) o then None
            else Some ((Int32.Parse o)::s)) (Some [])
    |> Option.bind (List.rev >> Some)
