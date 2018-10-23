using System;
using System.Linq;

public class Matrix
{
    private readonly int[][] matrix;

    public Matrix(string input) => 
        matrix = input.Split('\n')
            .Select(r => r.Split(' ')
                .Select(c => int.Parse(c)).ToArray()).ToArray();

    public int Rows => matrix.Length;

    public int Cols => matrix.Length > 0 ? matrix[0].Length : 0;

    public int[] Row(int row) => matrix[row];

    public int[] Column(int col) => Enumerable.Range(0, Rows).Select(r => matrix[r][col]).ToArray();
}