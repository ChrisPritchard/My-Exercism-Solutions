using System;
using System.Linq;
using System.Collections.Generic;

public class HangmanState
{
    public HangmanStatus Status { get; set; }
    public int RemainingGuesses { get; set; }
    public string MaskedWord { get; set; }
    public HashSet<char> Guesses { get; set; }
}

public enum HangmanStatus
{
    Busy, Win, Lose
}

public class HangmanGame
{
    public event EventHandler<HangmanState> StateChanged;

    private readonly string unmaskedWord;
    private HangmanState state;

    public HangmanGame(string word) => unmaskedWord = word;

    public void Start()
    {
        state = new HangmanState
        {
            Status = HangmanStatus.Busy,
            RemainingGuesses = 9,
            MaskedWord = new string('_', unmaskedWord.Length),
            Guesses = new HashSet<char>()
        };
        StateChanged?.Invoke(this, state);
    }

    public void Guess(char c)
    {
        if(state.Status != HangmanStatus.Busy)
            return;

        state.Guesses.Add(c);
        if(state.MaskedWord.Contains(c) || !unmaskedWord.Contains(c))
            if(state.RemainingGuesses == 0)
                state.Status = HangmanStatus.Lose;
            else
                state.RemainingGuesses --;
        else
            state.MaskedWord = new string(unmaskedWord.Select(oc => 
                state.Guesses.Contains(oc) ? oc : '_').ToArray());

        if(!state.MaskedWord.Contains('_'))
            state.Status = HangmanStatus.Win;

        StateChanged?.Invoke(this, state);
    }
}