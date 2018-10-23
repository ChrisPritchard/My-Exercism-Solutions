module Hangman
open System

type Progress = 
    | Busy of int
    | Win
    | Lose

type State = { progress: Progress; maskedWord: string }

type HangmanGame (word:string) =

    let word = word |> Seq.toList
    let guess = new Event<char>()
    let stateChange = new Event<State>()

    let initialState = { 
        maskedWord = word |> Seq.map (fun _ -> '_') |> String.Concat; 
        progress = Busy 9 
    }

    let advanceGame (state:State) (c:char) =
        match state.progress with
        | Win | Lose -> state
        | Busy n ->
            if state.maskedWord.Contains (string c) || Seq.contains c word |> not then
                let newProgress = n - 1
                { state with progress = if newProgress = 0 then Lose else Busy newProgress }
            else
                let indexes = [0..word.Length - 1] |> List.filter (fun i -> word.[i] = c)
                let newWord = 
                    state.maskedWord 
                    |> Seq.mapi (fun i cs -> if List.contains i indexes then c else cs) 
                    |> String.Concat
                if newWord.Contains "_" then
                    { state with maskedWord = newWord }
                else
                    { state with maskedWord = newWord; progress = Win }

    member _this.Guess c = guess.Trigger c
    member _this.StateChange = stateChange.Publish

    member _this.Start = 
        stateChange.Trigger initialState
        guess.Publish
        |> Event.scan advanceGame initialState
        |> Observable.add (stateChange.Trigger)

let createGame word = new HangmanGame (word)

let statesObservable (game: HangmanGame) = game.StateChange

let startGame (game: HangmanGame) = game.Start

let makeGuess c (game: HangmanGame) = game.Guess c