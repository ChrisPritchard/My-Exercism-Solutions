using System;
using System.Collections.Generic;
using System.Linq;

public class TreeBuildingRecord
{
    public int ParentId { get; set; }
    public int RecordId { get; set; }
}

public class Tree
{
    public int Id { get; set; }
    public int ParentId { get; set; }
    public List<Tree> Children { get; set; }

    public Tree(int id, int parentId, IEnumerable<Tree> children)
        => (Id, ParentId, Children) = (id, parentId, children.ToList());

    public bool IsLeaf => Children.Count == 0;
}

public static class TreeBuilder
{
    public static Tree BuildTree(IEnumerable<TreeBuildingRecord> records)
    {
        var children = new Dictionary<int, IEnumerable<Tree>>();
        void addChild(int key, Tree item) => 
            children[key] = children.ContainsKey(key) ? children[key].Append(item) : new [] { item };
        IEnumerable<Tree> forParent(int key) => 
            children.ContainsKey(key) ? children[key].Reverse() : Enumerable.Empty<Tree>();

        var trees = records.OrderByDescending(o => o.RecordId).Select(o => 
        {
            if (o.RecordId != 0 && o.ParentId >= o.RecordId)
                throw new ArgumentException();
            var tree = new Tree(o.RecordId, o.ParentId, forParent(o.RecordId));
            addChild(tree.ParentId, tree);
            return tree;
        }).Reverse().ToList();

        if(trees.Count == 0 
        || (trees[0].Id != 0 || trees[0].ParentId != 0)
        || trees.Count != trees.Last().Id + 1)
            throw new ArgumentException();

        return trees[0];
    }
}