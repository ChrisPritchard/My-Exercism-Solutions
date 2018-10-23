using System;

public class Deque<T>
{
    class Node
    {
        public T Value { get; set; }
        public Node Prev { get; set; }
        public Node Next { get; set; }
    }

    private Node root;

    public void Push(T value)
    {
        var pushed = new Node { Value = value };
        if(root != null)
        {
            root.Prev = pushed;
            pushed.Next = root;
        }
        root = pushed;
    }

    public T Pop()
    {
        if(root.Next != null)
            root.Next.Prev = null;
        var popped = root.Value;
        root = root.Next;
        return popped;
    }

    public void Unshift(T value)
    {
        if(root != null)
        {
            var last = root;
            while(last.Next != null)
                last = last.Next;
            last.Next = new Node { Value = value, Prev = last };
        }
        else root = new Node { Value = value };
    }

    public T Shift()
    {
        var last = root;
        while(last.Next != null)
            last = last.Next;
        if(last.Prev != null)
            last.Prev.Next = null;
        return last.Value;
    }
}