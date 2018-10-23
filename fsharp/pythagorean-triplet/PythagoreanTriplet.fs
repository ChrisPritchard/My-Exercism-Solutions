module PythagoreanTriplet

let triplet x y z = (x,y,z)

let isPythagorean triplet = 
    let (x,y,z) = triplet 
    let sorted = [x;y;z] |> List.sort
    pown sorted.[0] 2 + pown sorted.[1] 2 = pown sorted.[2] 2

let pythagoreanTriplets min max = 
    [min..max] |> Seq.collect (fun x -> [x+1..max] |> Seq.collect (fun y -> [y+1..max] |> Seq.map (fun z -> (x, y, z))))
    |> Seq.filter isPythagorean |> Seq.toList