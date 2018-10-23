module ZebraPuzzle

let people = ["Englishman";"Ukrainian";"Spaniard";"Norwegian";"Japanese"]
let smokes = ["Old Gold";"Kools";"Chesterfields";"Lucky Strike";"Parliaments"]
let drinks = ["Coffee";"Tea";"Milk";"Orange Juice";"Water"]
let pets = ["Dog";"Snails";"Fox";"Horse";"Zebra"]
let houses = ["Red";"Green";"Ivory";"Yellow";"Blue"]
let positions = ["Far Left";"Left";"Middle";"Right";"Far Right"]

let rules = [
    ("Englishman","Red");
    ("Spaniard","Dog");
    ("Coffee","Green");
    ("Ukrainian","Tea");
    ("Old Gold","Snails");
    ("Kools","Yellow");
    ("Milk","Middle");
    ("Norwegian","Far Left");
    ("Lucky Strike","Orange Juice");
    ("Japanese","Parliaments");
    ("Blue","Left");
]

let orRules = [
    ("Green", "Far Left");
    ("Ivory", "Far Right");
    ("Chesterfields", "Fox");
    ("Kools", "Horse");
    ("Norwegian", "Blue");
]

let validForRules lst =
    let (?=) lst o = Seq.contains o lst
    let andRules = 
        rules |> Seq.forall (fun (a,b) -> 
        not (lst ?= a || lst ?= b)
        || (lst ?= a && lst ?= b))
    andRules && 
        orRules |> Seq.forall (fun (a,b) -> 
        not (lst ?= a || lst ?= b)
        || not (lst ?= a && lst ?= b))

let validForLocation (sets:string list list) =
    let pos key = 
        sets |> List.find (fun x -> List.contains key x) |> List.last
        |> (fun p -> List.findIndex ((=) p) positions)
    pos "Green" - pos "Ivory" = 1 
    && abs (pos "Chesterfields" - pos "Fox") <= 1
    && abs (pos "Kools" - pos "Horse") <= 1

let range (lst: 'a seq) (func: 'a -> 'b seq) = Seq.collect func lst

let fullSets = 
    (range people <| fun pp -> 
    range smokes <| fun s -> 
    range drinks <| fun d -> 
    range pets <| fun p -> 
    range houses <| fun h -> 
    Seq.map (fun pos -> [pp;s;d;p;h;pos]) positions) 
    |> Seq.filter validForRules
    |> Seq.groupBy List.head |> (Seq.map snd >> Seq.toList)

let finalSet =
    (range fullSets.[0] <| fun en -> 
    range fullSets.[1] <| fun uk -> 
    range fullSets.[2] <| fun sp -> 
    range fullSets.[3] <| fun no -> 
    Seq.map (fun jp -> [en;uk;sp;no;jp]) fullSets.[4])
    |> Seq.filter (fun s -> List.concat s |> List.length = (List.concat s |> List.distinct |> List.length))
    |> Seq.filter validForLocation
    |> Seq.toList |> List.concat

type Person = | Englishman | Ukrainian | Spaniard | Norwegian | Japanese
let stringMap =
    function
    | "Englishman" -> Englishman
    | "Ukrainian" -> Ukrainian
    | "Spaniard" -> Spaniard
    | "Norwegian" -> Norwegian
    | _ -> Japanese

let drinksWater = finalSet |> List.find (fun x -> List.contains "Water" x)|> List.head |> stringMap
let ownsZebra = finalSet |> List.find (fun x -> List.contains "Zebra" x) |> List.head |> stringMap