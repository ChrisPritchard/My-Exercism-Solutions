module Anagram

let anagrams sources (target:string) =
    let targetSorted = Seq.sort (target.ToLower())
    let compareToTarget (x:string) = Seq.forall2 (=) (Seq.sort (x.ToLower())) targetSorted
    let notTarget (x:string) = x.ToLower() <> target.ToLower()
    List.filter (fun (x:string) -> x.Length = target.Length && notTarget x && compareToTarget x) sources
