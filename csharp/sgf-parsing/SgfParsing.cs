using System;
using System.Collections.Generic;
using System.Linq;
using Sprache;

public class SgfTree
{
    public SgfTree(IDictionary<string, string[]> data, params SgfTree[] children)
        => (Data, Children) = (data, children);

    public IDictionary<string, string[]> Data { get; }
    public SgfTree[] Children { get; }

    public override bool Equals(object obj)
    {
        var that = obj as SgfTree;
        if(that == null)
            return false;
        if(this.Data.Count != that.Data.Count || this.Children.Length != that.Children.Length)
            return false;
        if(this.Data.Keys.Any(key => !that.Data.ContainsKey(key) || !this.Data[key].SequenceEqual(that.Data[key])))
            return false;
        return this.Children.All(child => that.Children.Contains(child));
    }

    public override int GetHashCode() => base.GetHashCode();
}

public class SgfParser
{
    public static SgfTree ParseTree(string input)
    {
        try { return tree.Parse(input); }
        catch(ParseException) { throw new ArgumentException("invalid input"); }
    }

    private static readonly Parser<SgfTree> tree =
        from _ in Parse.Char('(')
        from node in node
        from __ in Parse.Char(')')
        select node;

    private static readonly Parser<SgfTree> node =
        from _ in Parse.Char(';')
        from attributes in Parse.Many(attribute)
        from children in Parse.Once(node).Or(Parse.Many(tree))
        select new SgfTree(attributes.ToDictionary(o => o.Item1, o => o.Item2), children.ToArray());
    
    private static readonly Parser<(string, string[])> attribute =
        from name in Parse.Many(Parse.Char(Char.IsUpper, "upper only")).Text()
        from values in Parse.AtLeastOnce(attributeValue)
        select (name, values.ToArray());

    private static readonly Parser<string> attributeValue = 
        from _ in Parse.Char('[')
        from value in Parse.Until(validPropValues, Parse.Char(']')).Text()
        select value;

    private static readonly Parser<char> validPropValues =
           (Parse.String("\\n").Return('\n'))
        .Or(Parse.String("\\t").Return(' '))
        .Or(Parse.Char('\\').Then(c => Parse.AnyChar))
        .Or(Parse.Char(' '))
        .Or(Parse.Letter);
    }