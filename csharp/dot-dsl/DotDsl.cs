using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public interface IGraphChild { }

public class Node : IGraphChild, IEnumerable<Attr>
{
    private List<Attr> attributes = new List<Attr>();

    public string Name { get; }

    public Node(string name) => Name = name;

    public void Add(string name, string value) => attributes.Add(new Attr(name, value));

    public IEnumerator<Attr> GetEnumerator() => attributes.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public override bool Equals(object other) => other is Node && (other as Node).Name == Name;
    public override int GetHashCode() => Name.GetHashCode();
}

public class Edge : IGraphChild, IEnumerable<Attr>
{
    private List<Attr> attributes = new List<Attr>();

    public string Node1 { get; }
    public string Node2 { get; }

    public Edge(string node1, string node2) =>
        (Node1, Node2) = (node1, node2);

    public void Add(string name, string value) => attributes.Add(new Attr(name, value));

    public IEnumerator<Attr> GetEnumerator() => attributes.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public override bool Equals(object other) => 
        other is Edge && (other as Edge).Node1 == Node1 && (other as Edge).Node2 == Node2;
    public override int GetHashCode() => Node1.GetHashCode() ^ Node2.GetHashCode();
}

public struct Attr : IGraphChild
{
    public string Name { get; }
    public string Value { get; }

    public Attr(string name, string value) =>
        (Name, Value) = (name, value);

    public override int GetHashCode() => Name.GetHashCode();
}

public class Graph : IEnumerable<IGraphChild>
{
    private List<IGraphChild> children = new List<IGraphChild>();

    public Node[] Nodes => children.OfType<Node>().OrderBy(o => o.Name).ToArray();
    public Edge[] Edges => children.OfType<Edge>().OrderBy(o => o.Node1).ToArray();
    public Attr[] Attrs => children.OfType<Attr>().OrderBy(o => o.Name).ToArray();

    public void Add(IGraphChild child) => children.Add(child);
    public void Add(string name, string value) => children.Add(new Attr(name, value));

    public IEnumerator<IGraphChild> GetEnumerator() => children.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}