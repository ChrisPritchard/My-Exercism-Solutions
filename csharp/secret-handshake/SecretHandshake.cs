using System;
using System.Linq;

public static class SecretHandshake
{
    private static readonly (int code, string text)[] commands = new[]
    { (1, "wink"), (2, "double blink"), 
      (4, "close your eyes"), (8, "jump") };

    private const int reverseCode = 16;

    public static string[] Commands(int commandValue)
    {
        var result = commands
            .Where(c => (c.code & commandValue) == c.code)
            .Select(c => c.text);

        return (reverseCode & commandValue) == reverseCode 
            ? result.Reverse().ToArray() 
            : result.ToArray();
    }
}
