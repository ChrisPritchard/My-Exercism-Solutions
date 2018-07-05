module ScaleGenerator
open System

let allPitches = ["A"; "A#"; "B"; "C"; "C#"; "D"; "D#"; "E"; "F"; "F#"; "G"; "G#"]
let flatPitches = ["A"; "Bb"; "B"; "C"; "Db"; "D"; "Eb"; "E"; "F"; "Gb"; "G"; "Ab"]
let useFlats = ["F"; "Bb"; "Eb"; "Ab"; "Db"; "Gb"; "d"; "g"; "c"; "f"; "bb"; "eb"]

let pitchListFor tonic = 
    match List.contains tonic useFlats with
    | true -> flatPitches
    | false -> allPitches

let pascalCase (tonic:string) = 
    if tonic.Length = 1 then tonic.ToUpper() 
    else tonic.[0].ToString().ToUpper() + tonic.Substring(1)

let pitches (tonic:string) (intervals: string Option) = 
    let pitchList = pitchListFor tonic
    let tonic = pascalCase tonic

    let rec next current remaining = seq {
        let nextIndex = 
            List.findIndex ((=) current) pitchList + 
                match List.head remaining with
                | 'M' -> 2
                | 'A' -> 3
                | _ -> 1
        let adjusted = 
            if nextIndex >= pitchList.Length 
            then nextIndex - pitchList.Length else nextIndex

        let newPitch = pitchList.[adjusted]

        match newPitch with
        | _ when newPitch = tonic -> ()
        | _ -> yield newPitch

        match List.tail remaining with
        | [] -> ()
        | t -> yield! next newPitch t
    }

    let remaining = (match intervals with | None -> "mmmmmmmmmmmm" | Some r -> r) |> Seq.toList
    [tonic] @ (next tonic remaining |> Seq.toList)
