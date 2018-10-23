module Markdown

let parse (markdown:string) =
    let rec parse (text:string) =
        if text.Length < 2 then text else 
        match Seq.take 2 text |> Seq.toList with
        | ['_';'_'] -> 
            let infix = text.[2..].IndexOf("__") + 1
            let target = text.[2..infix]
            let next = text.[target.Length + 4..]
            sprintf "<strong>%s</strong>%s" (parse target) (parse next)
        | ['_';_] -> 
            let infix = text.[1..].IndexOf("_")
            let target = text.[1..infix]
            let next = text.[target.Length + 2..]
            sprintf "<em>%s</em>%s" (parse target) (parse next)
        | ['*';' '] -> sprintf "<li>%s</li>" (parse text.[2..])
        | ['#';_] ->
            let size = Seq.filter ((=) '#') text.[0..5] |> Seq.length
            sprintf "<h%i>%s</h%i>" size (parse text.[size + 1..]) size
        | _ -> 
            let infix = text.IndexOf('_')
            if infix < 1 then text else text.[0..infix - 1] + (parse text.[infix..])

    let folder (html,isList) line = 
        let parsed = parse line
        match Seq.take 2 parsed |> Seq.toList, isList with
        | ['<'; 'l' ], true -> (html + parsed, true)
        | ['<'; 'l' ], false -> (html + "<ul>" + parsed, true)
        | ['<'; 'h' ], true -> (html + "</ul>" + parsed, false)
        | ['<'; 'h' ], false -> (html + parsed, false)
        | [_;_], true -> (html + "</ul><p>" + parsed + "</p>", false)
        | _ -> (html + "<p>" + parsed + "</p>", false)

    let state = markdown.Split('\n') |> Seq.fold folder ("", false)
    if snd state then fst state + "</ul>" else fst state