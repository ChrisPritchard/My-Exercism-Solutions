module SpaceAge

type Planet = EarthYear of float

let Earth = EarthYear 1.
let Mercury = EarthYear 0.2408467
let Venus = EarthYear 0.61519726
let Mars = EarthYear 1.8808158
let Jupiter = EarthYear 11.862615
let Saturn = EarthYear 29.447498
let Uranus = EarthYear 84.016846
let Neptune = EarthYear 164.79132

let secondsInEarthYear = 31557600.

let age (EarthYear earthYear) (seconds: int64): float = 
    let round2 (x:float) = System.Math.Round (x, 2)
    float seconds / (earthYear * secondsInEarthYear) |> round2