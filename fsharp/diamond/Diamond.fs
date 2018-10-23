module Diamond

let formatLine width (letter: char) =
    let distance = int letter - (int 'A')
    let core = 
        if distance = 0 then string letter
        else
            let coreSpace = String.replicate (-1 + distance * 2) " "
            sprintf "%c%s%c" letter coreSpace letter
    let sideSpace = 
        (width - (core.Length)) / 2 |> fun c -> String.replicate c " "
    sprintf "%s%s%s" sideSpace core sideSpace

let make (letter: char) =
    let letters = ['A'..letter]
    letters @ (letters |> List.rev |> List.tail)
        |> List.map (formatLine (-1 + (letters.Length * 2)))
        |> String.concat "\n"
