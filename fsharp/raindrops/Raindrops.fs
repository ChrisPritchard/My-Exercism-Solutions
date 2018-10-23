module Raindrops

let factors = [3,"Pling";5,"Plang";7,"Plong"]

let convert (number: int): string = 
    let factorSounds = 
        factors 
        |> List.filter (fun (factor,_) -> number % factor = 0) 
        |> List.map (fun (_,sound) -> sound) 
        |> String.concat ""
    match factorSounds with
    | s when s.Length > 0 -> s
    | _ -> string number