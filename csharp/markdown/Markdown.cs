using System;
using System.Linq;
using System.Text.RegularExpressions;

public static class Markdown
{
    private static string Wrap(string text, string tag) => $"<{tag}>{text}</{tag}>";

    private static string ParseText(string markdown, bool isList)
    {
        string Parse(string input, string delimiter, string tag)
        {
            var pattern = $"{delimiter}(.+){delimiter}";
            var replacement = $"<{tag}>$1</{tag}>";
            return Regex.Replace(input, pattern, replacement);
        }

        var parsedText = Parse(Parse(markdown, "__", "strong"), "_", "em");
        return isList ? parsedText : Wrap(parsedText, "p");
    }

    private static (string result, bool isList) ParseHeader(string markdown, bool isList)
    {
        var count = markdown.Length - markdown.TrimStart('#').Length;
        var headerTag = "h" + count;
        var headerHtml = Wrap(markdown.Substring(count + 1), headerTag);
        return (isList ? "</ul>" + headerHtml : headerHtml, false);
    }

    private static (string result, bool isList) ParseLineItem(string markdown, bool isList)
    {
        var innerHtml = Wrap(ParseText(markdown.Substring(2), true), "li");
        return (isList ? innerHtml : "<ul>" + innerHtml, true);
    }

    private static (string result, bool isList) ParseParagraph(string markdown, bool isList)
    {
        var parsed = ParseText(markdown, isList);
        return (isList ? "</ul>" + parsed : parsed, false);
    }

    private static (string result, bool isList) ParseLine(string markdown, bool isList)
    {
        if(markdown.StartsWith("#"))
            return ParseHeader(markdown, isList);
        else if(markdown.StartsWith("*"))
            return ParseLineItem(markdown, isList);
        else 
            return ParseParagraph(markdown, isList);
    }

    public static string Parse(string markdown)
    {
        var (result, isList) = markdown.Split('\n')
            .Aggregate(("", false), (o, line) => 
            {
                var (current, stillList) = o;
                var (lineResult, listResult) = ParseLine(line, stillList);
                return (current + lineResult, listResult);
            });

        return isList ? result + "</ul>" : result;
    }
}