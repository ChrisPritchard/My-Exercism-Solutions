module RationalNumbers

type RationalNumber = { a: int; b: int }

let create numerator denominator = { a = numerator; b = denominator }

let reduce r = 
    let rn = r |> fun f -> if f.b < 0 then create (f.a * -1) (f.b * -1) else f
    [rn.b..(-1)..1] 
    |> List.fold (fun c i -> 
        if c.a % i = 0 && c.b % i = 0 
        then create (c.a / i) (c.b / i) else c) rn

let add r1 r2 = create ((r1.a * r2.b) + (r2.a * r1.b)) (r1.b * r2.b) |> reduce

let sub r1 r2 = create ((r1.a * r2.b) - (r2.a * r1.b)) (r1.b * r2.b) |> reduce

let mul r1 r2 = create (r1.a * r2.a) (r1.b * r2.b) |> reduce

let div r1 r2 = create (r1.a * r2.b) (r2.a * r1.b) |> reduce

let abs r = create (abs r.a) (abs r.b) |> reduce

let exprational n r =
    if n >= 0 then create (pown r.a n) (pown r.b n) |> reduce
    else
        let m = FSharp.Core.Operators.abs n 
        create (pown r.b m) (pown r.a m) |> reduce

let expreal r x = (pown (float x) r.a |> float) ** (1.0 / (float r.b))
