using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SimpleLinkedList<T> : IEnumerable<T>
{
    public SimpleLinkedList(T value) => Value = value;

    public SimpleLinkedList(IEnumerable<T> values)
    {
        Value = values.FirstOrDefault();
        var next = values.Skip(1);
        if(next.Any())
            Next = new SimpleLinkedList<T>(next);
    }

    public T Value { get; private set; }

    public SimpleLinkedList<T> Next { get; private set; }

    public SimpleLinkedList<T> Add(T value)
    {
        Next = new SimpleLinkedList<T>(value);
        return this;
    } 

    public IEnumerator<T> GetEnumerator()
    {
        yield return Value;
        if(Next == null)
            yield break;
        
        var enumerator = Next.GetEnumerator();
        while(enumerator.MoveNext())
            yield return enumerator.Current;
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}