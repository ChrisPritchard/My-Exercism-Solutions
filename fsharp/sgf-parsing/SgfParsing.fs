module SgfParsing
open FParsec.Primitives
open FParsec.CharParsers

type Data = Map<string, string list>
type Tree = Node of Data * Tree list

let mkTree (data: Data) (children: Tree list) =
    Node (data, children)

let bc c1 p c2 = between (pchar c1) (pchar c2) p

let validPropValues = 
    ((pstring @"\n" <|> pstring @"\r" <|> pstring @"\t") |>> fun _ -> ' ')
    <|> (pchar '\\' >>. anyChar)
    <|> (anyOf (['a'..'z'] @ ['A'..'Z'] @ [' ']))
let ppropvalue = bc '[' (many validPropValues |>> (List.toArray >> System.String)) ']'

let validPropKeys = ['A'..'Z']
let pprop = (anyOf validPropKeys |>> string) .>>. (many ppropvalue)

let pdata: Parser<Data,unit> = pchar ';' >>. (many pprop) |>> Map.ofList

let (ptree, ptreeimpl) = createParserForwardedToRef<Tree,Unit>()
let (psingleOrTree, psingleOrTreeimpl) = createParserForwardedToRef<Tree list,Unit>()

let pnode: Parser<Tree,unit> = pdata .>>. psingleOrTree |>> Node

do ptreeimpl := bc '(' pnode ')'
do psingleOrTreeimpl := (pnode |>> fun n -> [n]) <|> (many ptree)

let parseSgf (input:string): Tree option =
    match run ptree input with
    | Success (result,_,_) -> Some result
    | Failure _ -> None