module ComplexNumbers

type ComplexNumber = { r: float; i: float }

let create real imaginary = { r = real; i = imaginary }

let mul z1 z2 = { r = (z1.r * z2.r) - (z1.i * z2.i); i = (z1.i * z2.r) + (z1.r * z2.i) }

let add z1 z2 = { r = (z1.r + z2.r); i = (z1.i + z2.i) }

let sub z1 z2 = { r = (z1.r - z2.r); i = (z1.i - z2.i) }

let div z1 z2 = 
    let divisor = (pown z2.r 2) + (pown z2.i 2)
    { 
        r = ((z1.r * z2.r) + (z1.i * z2.i)) / divisor;
        i = ((z1.i * z2.r) - (z1.r * z2.i)) / divisor
    }

let abs z = (pown z.r 2) + (pown z.i 2) |> sqrt

let conjugate z = { r = z.r; i = z.i * -1.0 }

let real z = z.r

let imaginary z = z.i

let exp z = 
    let cheat = new System.Numerics.Complex(z.r, z.i) |> System.Numerics.Complex.Exp
    { r = cheat.Real; i = cheat.Imaginary }
