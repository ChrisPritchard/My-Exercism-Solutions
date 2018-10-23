using System;

public class CustomSet
{
    private int[] values;

    public CustomSet(params int[] values) => this.values = values;

    public CustomSet Add(int value) 
    {
        if(Contains(value))
            return this;
        var newValues = new int[values.Length + 1];
        values.CopyTo(newValues, 0);
        newValues[values.Length] = value;
        return new CustomSet(newValues);
    }

    public bool Empty() => values.Length == 0;

    public bool Contains(int value) => Array.IndexOf(values, value) != -1;

    public bool Subset(CustomSet right)
    {
        if(Empty()) 
            return true;
        if(right.values.Length < values.Length) 
            return false;

        bool theSame(int[] one, int[] two)
        {
            for(var i = 0; i < one.Length; i++)
                if(one[i] != two[i])
                    return false;
            return true;
        }

        for(var i = 0; i <= right.values.Length - values.Length; i++)
        {
            var other = new int[values.Length];
            Array.Copy(right.values, i, other, 0, other.Length);
            if(theSame(values, other))
               return true; 
        }

        return false;
    }

    public bool Disjoint(CustomSet right)
    {
        if(Empty() || right.Empty())
            return true;

        foreach(var value in values)
            foreach(var otherValue in right.values)
                if(value == otherValue)
                    return false;
        return true;
    }

    public CustomSet Intersection(CustomSet right)
    {
        var buffer = new int[values.Length];
        var count = 0;
        
        foreach(var value in values)
            foreach(var otherValue in right.values)
                if(value == otherValue)
                {
                    buffer[count] = value;
                    count++;
                }
        
        var intersection = new int[count];
        Array.Copy(buffer, intersection, count);

        return new CustomSet(intersection);
    }

    public CustomSet Difference(CustomSet right)
    {
        var buffer = new int[values.Length];
        var count = 0;
        
        foreach(var value in values)
        {
            if(right.Contains(value))
                continue;
            buffer[count] = value;
            count++;
        }

        var difference = new int[count];
        Array.Copy(buffer, difference, count);

        return new CustomSet(difference);
    }

    public CustomSet Union(CustomSet right)
    {
        var union = this;
        foreach(var otherValue in right.values)
            union = union.Add(otherValue);
        return union;
    }

    public override bool Equals(object obj)
    {
        var other = obj as CustomSet;
        if(other == null)
            return false;
        if(other.values.Length != values.Length)
            return false;

        foreach(var value in values)
        {
            var exists = false;
            foreach(var otherValue in other.values)
                if(otherValue == value)
                {
                    exists = true;
                    break;
                }
            if(!exists)
                return false;
        }

        return true;
    }

    public override int GetHashCode() => values.GetHashCode();
}