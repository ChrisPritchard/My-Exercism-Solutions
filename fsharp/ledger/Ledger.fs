module Ledger

open System
open System.Globalization

type Entry = { date: DateTime; description: string; change: int }

let mkEntry date description change = { 
        date = DateTime.Parse(date, CultureInfo.InvariantCulture); 
        description = description; change = change 
    }
        
let formatLedger currency (locale:string) entries = 
    let culture = new CultureInfo(locale)
    culture.NumberFormat.CurrencySymbol <- match currency with | "EUR" -> "€" | _ -> "$"
    let dateFormat = match locale with | "nl-NL" -> "dd-MM-yyyy" | _ -> "MM\/dd\/yyyy"

    let linePrinter entry = 
        let date = entry.date.ToString(dateFormat)     
        let description = 
            match entry.description.Length with
            | l when l > 25 ->  entry.description.[0..21] + "..."
            | _ -> entry.description
        let change = String.Format(culture, "{0:C}{1}", float entry.change / 100.0, 
                        match entry.change with | _ when entry.change < 0 -> "" | _ -> " ")

        sprintf "%s | %-25s | %13s" date description change

    System.String.Join("\n", seq {
        match locale with
        | "nl-NL" ->    yield "Datum      | Omschrijving              | Verandering  "
        | _ ->          yield "Date       | Description               | Change       "

        let sorted = List.sortBy (fun x -> x.date, x.description, x.change) entries
        yield! sorted |> List.map linePrinter
    })