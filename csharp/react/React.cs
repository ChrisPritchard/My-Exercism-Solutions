using System;
using System.Collections.Generic;
using System.Linq;

public class Reactor
{
    public InputCell CreateInputCell(int value) => 
        new InputCell(value);

    public ComputeCell CreateComputeCell(IEnumerable<Cell> producers, Func<int[], int> compute) => 
        new ComputeCell(producers, compute);
}

public abstract class Cell
{
    public virtual int Value { get; set; }
    public EventHandler<int> Changed;
}

public class InputCell : Cell
{
    private int value;
    public override int Value 
    { 
        get { return value; } 
        set 
        { 
            if(value == this.value)
                return;
            this.value = value; 
            Changed?.Invoke(this, value); 
        } 
    }

    public InputCell(int value) => Value = value;
}

public class ComputeCell : Cell
{
    private readonly IEnumerable<Cell> inputs;
    private readonly Func<int[], int> compute;

    private int GetValue() => compute(inputs.Select(o => o.Value).ToArray());

    private int oldValue;

    public override int Value { get => GetValue(); set => throw new InvalidOperationException(); }

    public ComputeCell(IEnumerable<Cell> inputs, Func<int[], int> compute)
    {
        this.inputs = inputs;
        this.compute = compute;    

        oldValue = GetValue();    

        foreach(var cell in inputs)
            cell.Changed += (o, e) =>
            {
                var newValue = GetValue();
                if(newValue == oldValue)
                    return;
                oldValue = newValue;
                Changed?.Invoke(this, newValue);
            };
    }
}