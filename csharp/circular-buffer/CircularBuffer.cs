using System;

public class CircularBuffer<T>
{
    T[] buffer;
    int oldest = -1, newest = -1;

    public CircularBuffer(int capacity) => buffer = new T[capacity];

    private bool Full() => newest == oldest - 1 || (oldest == 0 && newest == buffer.Length - 1);
    
    private void Increment(ref int source) => source = source == buffer.Length - 1 ? 0 : source + 1;

    public T Read()
    {
        if(oldest == -1)
            throw new InvalidOperationException();
        var value = buffer[oldest];
        if (oldest == newest)
            Clear();
        else 
            Increment(ref oldest);
        return value;
    }

    public void Write(T value)
    {
        if(Full())
            throw new InvalidOperationException();
        Increment(ref newest);
        if(oldest == -1)
            oldest = newest;
        buffer[newest] = value;
    }

    public void Overwrite(T value)
    {
        if(!Full())
            Write(value);
        else if(oldest == -1)
            throw new InvalidOperationException();
        else
        {
            buffer[oldest] = value;
            newest = oldest;
            Increment(ref oldest);
        }
    }

    public void Clear() => (buffer, oldest, newest) = (new T[buffer.Length], -1, -1);
}