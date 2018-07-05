module ListOps

let rec foldl folder state list = 
    match list with
    | [] -> state
    | head::tail -> foldl folder (folder state head) tail

let reverse list = foldl (fun acc i -> i::acc) [] list

let rec foldr folder state list = foldl (fun state t -> folder t state) state (reverse list)

let length list = foldl (fun acc _ -> acc + 1) 0 list

let map f list = foldr (fun i acc -> (f i)::acc) [] list

let filter f list = foldr (fun i acc -> if f i then i::acc else acc) [] list

let append xs ys = foldr (fun i acc -> i::acc) ys xs

let concat xs = foldr append [] xs
