module Clock

type Clock = | Minutes of int

let (%%) x y = y % x

let create hours minutes = 
    let withinDay m = if m < 0 then 1440 + m else m
    minutes + (hours * 60) |> (%%) 1440 |> withinDay |> Minutes

let add minutes clock = 
    let (Minutes existing) = clock
    create 0 (existing + minutes)

let subtract minutes clock = 
    let (Minutes existing) = clock
    create 0 (existing - minutes)

let display clock = 
    let (Minutes existing) = clock
    sprintf "%02i:%02i" 
        ((float existing) / 60.0 |> floor |> int |> (%%) 24) 
        (existing % 60)
