module ProteinTranslation

let proteins (rna:string) = 
    let codons = rna |> Seq.chunkBySize 3 |> Seq.map System.String.Concat
    let rec translate codonList outputList =
        if Seq.isEmpty codonList then outputList 
        else
            let remainder = Seq.tail codonList
            match Seq.head codonList with
            | "AUG" -> translate (Seq.tail codonList) (outputList @ ["Methionine"]) 
            | x when Seq.contains x ["UUU";"UUC"] -> translate remainder (outputList @ ["Phenylalanine"])
            | x when Seq.contains x ["UUA";"UUG"] -> translate remainder (outputList @ ["Leucine"])
            | x when Seq.contains x ["UCU";"UCC";"UCA";"UCG"] -> translate remainder (outputList @ ["Serine"])
            | x when Seq.contains x ["UAU";"UAC"] -> translate remainder (outputList @ ["Tyrosine"])
            | x when Seq.contains x ["UGU";"UGC"] -> translate remainder (outputList @ ["Cysteine"])
            | "UGG" -> translate remainder (outputList @ ["Tryptophan"])
            | _ -> outputList
    translate codons []
