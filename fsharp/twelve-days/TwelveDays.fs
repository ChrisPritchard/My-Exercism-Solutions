module TwelveDays

let recite start stop = 
    let days = ["first";"second";"third";"fourth";"fifth";"sixth";"seventh";"eighth";"ninth";"tenth";"eleventh";"twelfth"]
    let gifts = [
        "a Partridge in a Pear Tree";"two Turtle Doves";"three French Hens";
        "four Calling Birds";"five Gold Rings";"six Geese-a-Laying";
        "seven Swans-a-Swimming";"eight Maids-a-Milking";"nine Ladies Dancing";
        "ten Lords-a-Leaping";"eleven Pipers Piping";"twelve Drummers Drumming"
    ]
    let line day = 
        let phrases = [day - 1..-1..0] |> Seq.map (fun x -> if x = 0 && day <> 1 then "and " + gifts.[x] else gifts.[x])
        sprintf "On the %s day of Christmas my true love gave to me, %s." days.[day - 1] (System.String.Join (", ", phrases))
    [start..stop] |> List.map line