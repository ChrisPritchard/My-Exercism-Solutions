module BeerSong

let recite startBottles toTakeDown = 

    let rec reciteWithTailRecursion prior bottles takeDown = 
        let bottleLabel line = 
            match line with
            | 0 -> "No more bottles"
            | 1 -> "1 bottle"
            | _ -> sprintf "%d bottles" line

        let currentLabel = bottleLabel bottles
        let nextLabel = bottleLabel (bottles - 1)

        let lines = [
            sprintf "%s of beer on the wall, %s of beer." currentLabel (currentLabel.ToLower());
            (match bottles with
            | 0 -> "Go to the store and buy some more, 99 bottles of beer on the wall."
            | _ -> sprintf "Take %s down and pass it around, %s of beer on the wall." (if bottles = 1 then "it" else "one") (nextLabel.ToLower()))
        ]

        match takeDown with
        | _ when takeDown <= 1 -> [prior @ lines]
        | _ -> reciteWithTailRecursion (prior @ lines @ [""]) (bottles - 1) (takeDown - 1)

    reciteWithTailRecursion [] startBottles toTakeDown |> List.concat