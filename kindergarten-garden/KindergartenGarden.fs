module KindergartenGarden

let students = ["Alice";"Bob";"Charlie";"David";"Eve";"Fred";"Ginny";"Harriet";"Ileana";"Joseph";"Kincaid";"Larry"]

type Plant() =
    static member Grass = 'G'
    static member Clover = 'C'
    static member Radishes = 'R'
    static member Violets = 'V'

let plants (diagram:string) (student:string) = 
    let index = List.findIndex (fun s -> s = student) students |> (*) 2
    let chars = diagram.ToCharArray()
    let lineLength = Array.findIndex (fun c -> c = '\n') chars |> (+) 1
    Array.concat [
        chars |> Array.skip index |> Array.take 2;
        chars |> Array.skip (lineLength + index) |> Array.take 2
    ] |> Array.toList
