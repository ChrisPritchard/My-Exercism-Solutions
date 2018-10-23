module ArmstrongNumbers

let isArmstrongNumber (number: int): bool = 
    let chars = [for c in string number -> float (c.ToString())]
    let length = float chars.Length
    chars |> List.sumBy (fun x -> int (x ** length)) |> (=) number