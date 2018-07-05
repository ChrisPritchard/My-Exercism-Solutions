module Bob

let response (input: string): string = 
    let trimmed = input.Trim()
    let trimmedChars = trimmed.ToCharArray()
    let isQuestion = 
        Array.tryLast trimmedChars = Some '?'

    let isLetter (x:char) = System.Char.IsLetter(x)
    let isUpper =
        let matches = trimmed.ToUpper() = trimmed
        let hasLetters = Array.tryFind isLetter trimmedChars <> None
        matches && hasLetters
    let notControl (x:char) = not (System.Char.IsControl(x))

    match trimmed with
    | _ when trimmed.Length = 0 || Array.tryFind notControl trimmedChars = None -> "Fine. Be that way!"
    | _ when isUpper && isQuestion -> "Calm down, I know what I'm doing!"
    | _ when isUpper -> "Whoa, chill out!"
    | _ when isQuestion -> "Sure."
    | _ -> "Whatever."
