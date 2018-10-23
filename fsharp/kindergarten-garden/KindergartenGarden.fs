module KindergartenGarden


type Plant = | Grass | Clover | Radishes | Violets

let asPlant = function | 'G' -> Grass | 'C' -> Clover | 'R' -> Radishes | 'V' -> Violets | _ -> failwith "unrecognised plant"

let students = ["Alice";"Bob";"Charlie";"David";"Eve";"Fred";"Ginny";"Harriet";"Ileana";"Joseph";"Kincaid";"Larry"]

let plants diagram student = 
    let index = 2 * Seq.findIndex ((=) student) students
    let lineLength = 1 + Seq.findIndex ((=) '\n') diagram
    Seq.concat [
        diagram |> Seq.skip index |> Seq.take 2;
        diagram |> Seq.skip (lineLength + index) |> Seq.take 2
    ] |> Seq.map asPlant |> Seq.toList