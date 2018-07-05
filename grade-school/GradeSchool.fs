module GradeSchool

let empty: Map<int, string list> = 
    Map.empty<int, string list>

let add (student: string) (grade: int) (school: Map<int, string list>): Map<int, string list> = 
    let newGrade = 
        match school |> Map.tryFind grade with
        | Some students -> List.append [student] students |> List.sort
        | None -> [student]
    school |> Map.add grade newGrade

let roster (school: Map<int, string list>): (int * string list) list = 
    school |> Map.toList |> List.sortBy fst

let grade (number: int) (school: Map<int, string list>): string list = 
    match school |> Map.tryFind number with
    | Some students -> students |> List.sort
    | None -> []
