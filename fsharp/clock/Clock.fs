module Clock

type Clock = | Minutes of int

let create hours minutes = 
    let withinDay m = if m < 0 then 1440 + m else m
    let totalMinutes = minutes + (hours * 60)
    totalMinutes % 1440 |> withinDay |> Minutes

let add minutes (Minutes existing) = create 0 (existing + minutes)

let subtract minutes (Minutes existing) = create 0 (existing - minutes)

let display (Minutes totalMinutes) = 
    let totalHours = float totalMinutes / 60. |> floor |> int
    sprintf "%02i:%02i" <|| (totalHours % 24, totalMinutes % 60)