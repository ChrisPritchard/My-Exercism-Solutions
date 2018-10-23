using System;
using System.IO;
using System.Linq;

public static class Grep
{
    public static string Match(string pattern, string flags, string[] files)
    {
        Func<(string file, int index, string text), bool> matcher = line => line.text.Contains(pattern);
        Func<(string file, int index, string text), string> printer = line => line.text;

        bool flag(string token) => flags.Contains(token);

        if(flag("-x"))
            matcher = line => line.text == pattern;
        if(flag("-i"))
        {
            pattern = pattern.ToLower();
            var innerMatcher = matcher;
            matcher = line => innerMatcher((line.file, line.index, line.text.ToLower()));
        }
        if(flag("-v"))
        {
            var innerMatcher = matcher;
            matcher = line => !innerMatcher(line);
        }   

        if(flag("-n"))
            printer = line => $"{line.index + 1}:{line.text}";
        if(flag("-l"))
            printer = line => line.file;
        else if(files.Length > 1)
        {
            var innerPrinter = printer;
            printer = line => $"{line.file}:{innerPrinter(line)}";
        }

        var results = files
            .SelectMany(fileName => 
                File.ReadAllLines(fileName).Select((line, index) => 
                    (fileName, index, line)))
            .Where(matcher).Select(printer).Distinct();

        return string.Join("\n", results);
    }
}