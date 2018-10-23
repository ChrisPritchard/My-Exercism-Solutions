module Say

let divide (number: int64) power = 
    let devisor = 10.0 ** power |> float
    let number = float number
    (number / devisor |> floor |> int64, number % devisor |> int64)

let underTwenty number = 
    match number with
    | 1L -> "one" | 2L -> "two" | 3L -> "three" | 4L -> "four" | 5L -> "five"
    | 6L -> "six" | 7L -> "seven" | 8L -> "eight" | 9L -> "nine" | 10L -> "ten"
    | 11L -> "eleven" | 12L -> "twelve" | 13L -> "thirteen" | 14L -> "fourteen"
    | 15L -> "fifteen" | 16L -> "sixteen" | 17L -> "seventeen" | 18L -> "eighteen"
    | 19L -> "nineteen" | _ -> ""

let underHundred number =
    let (tens, remainder) = divide number 1.0
    let result =
        match tens with
        | 2L -> sprintf "twenty-%s" (underTwenty remainder)
        | 3L -> sprintf "thirty-%s" (underTwenty remainder)
        | 4L -> sprintf "forty-%s" (underTwenty remainder)
        | 5L -> sprintf "fifty-%s" (underTwenty remainder)
        | 6L -> sprintf "sixty-%s" (underTwenty remainder)
        | 7L -> sprintf "seventy-%s" (underTwenty remainder)
        | 8L -> sprintf "eighty-%s" (underTwenty remainder)
        | 9L -> sprintf "ninety-%s" (underTwenty remainder)
        | _ -> underTwenty number
    result.TrimEnd '-'

let underThousand number = 
    let (hundreds, remainder) = divide number 2.0
    match hundreds with
    | 0L -> underHundred remainder
    | n -> sprintf "%s hundred %s" (underTwenty n) (underHundred remainder)

let say number =
    if number < 0L || number >= 1000000000000L then None
    elif number = 0L then Some "zero"
    else
        let (billions, remainder) = divide number 9.0
        let (millions, remainder) = divide remainder 6.0
        let (thousands, remainder) = divide remainder 3.0
        [
            (if billions > 0L then sprintf "%s billion" (underThousand billions) else "");
            (if millions > 0L then sprintf "%s million" (underThousand millions) else "");
            (if thousands > 0L then sprintf "%s thousand" (underThousand thousands) else "");
            (underThousand remainder)
        ] |> List.filter ((=) "" >> not) |> String.concat " " |> fun s -> Some (s.Trim())
