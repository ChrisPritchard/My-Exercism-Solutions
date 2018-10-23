module RnaTranscription

let toRna (dna: string): string option = 
    let dna2rna x =
        match x with
        | 'G' -> 'C'
        | 'C' -> 'G'
        | 'T' -> 'A'
        | 'A' -> 'U'
        | _ -> '?'
    let rna = dna |> Seq.map dna2rna |> System.String.Concat
    match rna with
    | _ when rna.IndexOf("?") >= 0 -> None
    | _ -> Some rna