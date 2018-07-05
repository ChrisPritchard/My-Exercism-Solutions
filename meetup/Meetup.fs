module Meetup

open System

type Week = First | Second | Third | Fourth | Last | Teenth

let meetup year month week dayOfWeek: DateTime = 
    let findDay range = 
        range
        |> Seq.map (fun x -> new DateTime(year, month, x)) 
        |> Seq.findBack (fun (x:DateTime) -> x.DayOfWeek = dayOfWeek)
    match week with
    | First -> findDay [1..7]
    | Second -> findDay [7..14]
    | Third -> findDay [14..21]
    | Fourth -> findDay [21..28]
    | Last -> 
        let lastDay = (new DateTime(year,month,1)).AddMonths(1).AddDays(-1.0).Day
        findDay [21..lastDay]
    | Teenth -> findDay [13..19]
