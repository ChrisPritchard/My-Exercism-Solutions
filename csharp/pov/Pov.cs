using System;
using System.Collections.Generic;
using System.Linq;

public class Tree
{
    public string Value { get; }
    public Tree[] Children { get; }
    
    public Tree(string value, params Tree[] children) => (Value, Children) = (value, children);

    public override bool Equals(object obj) => 
        obj is Tree 
        && (obj as Tree).Value == Value 
        && (obj as Tree).Children.OrderBy(o => o.Value)
            .SequenceEqual(Children.OrderBy(o => o.Value));

    public override int GetHashCode() => base.GetHashCode();
}

public static class Pov
{
    public static Tree FromPov(Tree tree, string from)
    {
        var path = Find(tree, from, Enumerable.Empty<Tree>());
        if(path == null)
            throw new ArgumentException();

        Tree Reparent(Tree child, Tree parent)
        {
            var newChild = new Tree(parent.Value, parent.Children.Where(o => o.Value != child.Value).ToArray());
            return new Tree(child.Value, child.Children.Append(newChild).ToArray());
        }

        var current = path.First();
        foreach(var child in path.Skip(1))
            current = Reparent(child, current);
        return current;
    }

    public static IEnumerable<string> PathTo(string from, string to, Tree tree) => 
        Find(FromPov(tree, from), to, Enumerable.Empty<Tree>())?.Select(o => o.Value) ?? throw new ArgumentException();

    private static IEnumerable<Tree> Find(Tree current, string target, IEnumerable<Tree> soFar)
    {
        soFar = soFar.Append(current);
        if(current.Value == target)
            return soFar;
        if(current.Children.Length == 0)
            return null;
        return current.Children.Select(child => 
            Find(child, target, soFar))
            .FirstOrDefault(result => result != null);
    }
}