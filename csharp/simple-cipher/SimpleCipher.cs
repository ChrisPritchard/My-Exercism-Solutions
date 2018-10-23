using System;
using System.Linq;

public class SimpleCipher
{
    private readonly int min = (int)'a';
    private readonly int max = (int)'z';
    
    public string Key { get; }

    public SimpleCipher()
    {
        var random = new Random();
        Key = string.Concat(Enumerable.Range(0, 10).Select(_ => (char)random.Next(min, max + 1)));
    }

    public SimpleCipher(string key)
    {
        if(string.IsNullOrWhiteSpace(key) || key.Any(c => !char.IsLetter(c) || !char.IsLower(c)))
            throw new ArgumentException();
        Key = key;
    }

    private int[] KeyForLength(int length) =>
        Enumerable.Range(0, length)
        .Select(i => Key[i % Key.Length] - min)
        .ToArray();

    private char Adjust(char source, int amount)
    {
        var result = source + amount;
        result = result > max ? result - 26 : (result < min ? result + 26 : result);
        return (char)result;
    }

    public string Encode(string plaintext)
    {
        var key = KeyForLength(plaintext.Length);
        return string.Concat(plaintext.Select((c, i) => Adjust(c, key[i])));
    }

    public string Decode(string ciphertext)
    {
        var key = KeyForLength(ciphertext.Length);
        return string.Concat(ciphertext.Select((c, i) => Adjust(c, -key[i])));
    }
}