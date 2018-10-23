using System;
using System.Linq;

public static class AffineCipher
{
    const int m = 26;

    public static string Encode(string plainText, int a, int b)
    {
        if(!AreCoPrime(a, m))
            throw new ArgumentException();

        int algo(char c) => ((c - 'a') * a + b) % m;
        char asChar(int cipher) => (char)(cipher + 'a');

        var cleaned = plainText.ToLower().Where(char.IsLetterOrDigit);
        var encoded = cleaned.Select(c => char.IsDigit(c) ? c : asChar(algo(c))).ToArray();
        var chunked = Enumerable.Range(0, encoded.Length / 5 + 1)
            .Select(i => new string(encoded.Skip(i * 5).Take(5).ToArray()));

        return string.Join(' ', chunked.ToArray()).Trim();
    }

    public static string Decode(string cipheredText, int a, int b)
    {
        if(!AreCoPrime(a, m))
            throw new ArgumentException();

        var modInverse = Enumerable.Range(1, m).First(i => i * a % m == 1);
        int algo(char c) => (modInverse * ((c - 'a') - b)) % m;
        char asChar(int cipher) => (char)((cipher < 0 ? 26 + cipher : cipher) + 'a');

        var cleaned = cipheredText.ToLower().Where(char.IsLetterOrDigit);
        var decoded = cleaned.Select(c => char.IsDigit(c) ? c : asChar(algo(c))).ToArray();

        return new string(decoded);
    }

    private static bool AreCoPrime(int value1, int value2)
    {
        while (value1 != 0 && value2 != 0)
        {
            if (value1 > value2)
                value1 %= value2;
            else
                value2 %= value1;
        }
        return Math.Max(value1, value2) == 1;
    }
}
