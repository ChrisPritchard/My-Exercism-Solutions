using System;
using System.Collections.Generic;
using System.Linq;

public class BinTree
{
    public BinTree(int value, BinTree left, BinTree right)
        => (Value, Left, Right) = (value, left, right);

    public int Value { get; }
    public BinTree Left { get; }
    public BinTree Right { get; }

    public override bool Equals(object obj)
    {
        var that = obj as BinTree;
        if(that == null || that.Value != Value)
            return false;
        var leftMatch = (that.Left == null && Left == null) || (Left != null && Left.Equals(that.Left));
        var rightMatch = (that.Right == null && Right == null) || (Right != null && Right.Equals(that.Right));
        return leftMatch && rightMatch;
    }

    public override int GetHashCode() => base.GetHashCode();
}

public class Zipper
{
    private BinTree focus;
    private IEnumerable<(BinTree node, bool isLeft)> parents;

    public int Value() => focus.Value;
    public Zipper Left() => focus.Left != null ? FromTree(focus.Left, parents.Prepend((focus, true))) : null;
    public Zipper Right() => focus.Right != null ? FromTree(focus.Right, parents.Prepend((focus, false))) : null;
    public Zipper Up()
    {
        if(!parents.Any())
            return null;
        var parent = parents.First();
        var others = parents.Skip(1);

        var newFocus = new BinTree(parent.node.Value, 
            parent.isLeft ? focus : parent.node.Left, 
            parent.isLeft ? parent.node.Right : focus);
        return FromTree(newFocus, others);
    }

    public Zipper SetValue(int newValue) => FromTree(new BinTree(newValue, focus.Left, focus.Right), parents);

    public Zipper SetLeft(BinTree binTree) => FromTree(new BinTree(focus.Value, binTree, focus.Right), parents);

    public Zipper SetRight(BinTree binTree) => FromTree(new BinTree(focus.Value, focus.Left, binTree), parents);

    public BinTree ToTree()
    {
        var top = this;
        Zipper next = null;
        while((next = top.Up()) != null)
            top = next;
        return top.focus;
    }

    public static Zipper FromTree(BinTree tree, IEnumerable<(BinTree, bool)> parents = null) => 
        new Zipper { focus = tree, parents = parents ?? Enumerable.Empty<(BinTree, bool)>() };

    public override bool Equals(object obj) => (obj as Zipper).focus.Equals(focus);

    public override int GetHashCode() => base.GetHashCode();
}