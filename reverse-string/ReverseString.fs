module ReverseString

let reverse (input: string): string = input |> Seq.rev |> System.String.Concat
