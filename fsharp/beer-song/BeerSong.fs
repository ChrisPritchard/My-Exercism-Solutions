module BeerSong

let rec recite (startBottles: int) (takeDown: int) = 
    let bottleLabel line = 
        match line with
        | 0 -> "No more bottles"
        | 1 -> "1 bottle"
        | _ -> sprintf "%d bottles" line
    let currentLabel = bottleLabel startBottles
    let nextLabel = bottleLabel (startBottles - 1)
    let lines = [
        sprintf "%s of beer on the wall, %s of beer." currentLabel (currentLabel.ToLower());
        (match startBottles with
        | 0 -> "Go to the store and buy some more, 99 bottles of beer on the wall."
        | _ -> sprintf "Take %s down and pass it around, %s of beer on the wall." (if startBottles = 1 then "it" else "one") (nextLabel.ToLower()))
    ]
    match takeDown with
    | _ when takeDown <= 1 -> lines
    | _ -> List.concat [lines;[""];recite (startBottles - 1) (takeDown - 1)]
