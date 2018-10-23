using System.Linq;

public class BinarySearch
{
    private readonly int[] input;

    public BinarySearch(int[] input) => this.input = input;

    public int Find(int value, int min = 0, int max = -1)
    {
        if (input.Length == 0)
            return -1;

        max = max == -1 ? input.Length - 1 : max;
        var mid = (max - min) / 2 + min;

        if (input[mid] == value)
            return mid;
        if (min == max)
            return -1;
            
        return input[mid] > value
            ? Find(value, min, mid - 1)
            : Find(value, mid + 1, max);
    }
}