module SimpleCipher
open System

let encode (key:string) (input:string) = 
    if Seq.exists (fun (c:char) -> not (Char.IsLetter c) || Char.IsUpper c) key 
        || key.Trim().Length = 0 then failwith "Invalid key"
    let cipher kc ic = 
        let adjust = (int kc) - 97
        let cc = int ic + adjust
        char <| if cc > 122 then cc - 26 else cc
    Seq.map2 cipher key input |> String.Concat

let decode (key:string) (input:string) = 
    let decipher kc cc = 
        let adjust = (int kc) - 97
        let oc = int cc - adjust
        char <| if oc < 97 then oc + 26 else oc
    Seq.map2 decipher key input |> String.Concat

let encodeRandom (input:string) = 
    let random = new Random()
    let key = [0..99] |> Seq.map (fun _ -> random.Next(97,122) |> char) |> String.Concat
    (key, encode key input)
