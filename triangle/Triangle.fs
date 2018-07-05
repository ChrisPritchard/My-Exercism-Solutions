module Triangle

let isTriangle triangle = 
    match List.length triangle = 3 && Seq.forall (fun x -> x > 0.0) triangle with
    | false -> false
    | true -> 
        let sorted = triangle |> List.sortDescending
        List.head sorted <= List.sum (List.tail sorted)

let uniqueSides triangle = List.distinct triangle |> List.length

let equilateral triangle = isTriangle triangle && uniqueSides triangle = 1

let isosceles triangle = isTriangle triangle && uniqueSides triangle < 3

let scalene triangle = isTriangle triangle && uniqueSides triangle = 3
