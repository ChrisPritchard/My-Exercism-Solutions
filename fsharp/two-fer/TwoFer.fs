module TwoFer

let twoFer (input: string option): string = 
    match input with
    | Some name -> sprintf "One for %s, one for me." name
    | None -> sprintf "One for you, one for me."