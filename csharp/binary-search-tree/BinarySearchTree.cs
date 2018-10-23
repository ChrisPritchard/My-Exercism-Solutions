using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class BinarySearchTree : IEnumerable<int>
{
    public BinarySearchTree(int value) => Value = value;

    public BinarySearchTree(IEnumerable<int> values) : this(values.First())
    {
        foreach(var value in values.Skip(1))
            Add(value);
    }

    public BinarySearchTree Add(int value)
    {
        if(value <= Value)
            Left = Left != null ? Left.Add(value) : new BinarySearchTree(value);
        else
            Right = Right != null ? Right.Add(value) : new BinarySearchTree(value);
        return this;
    }

    public int Value { get; }

    public BinarySearchTree Left { get; private set; }

    public BinarySearchTree Right { get; private set; }

    public IEnumerator<int> GetEnumerator()
    {
        if(Left != null)
        {
            var leftEnumerator = Left.GetEnumerator();
            while(leftEnumerator.MoveNext())
                yield return leftEnumerator.Current;
        }

        yield return Value;

        if(Right != null)
        {
            var rightEnumerator = Right.GetEnumerator();
            while(rightEnumerator.MoveNext())
                yield return rightEnumerator.Current;
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}