package matrix

import (
	"errors"
	"strconv"
	"strings"
)

// Matrix contains a list of rows and columns
type Matrix struct {
	rows [][]int
}

// New parses a string into a Matrix.
func New(in string) (*Matrix, error) {
	rowsText := strings.Split(in, "\n")
	rows := make([][]int, len(rowsText))

	colLength := -1
	for i, rowText := range rowsText {
		colsText := removeEmpty(strings.Split(rowText, " "))

		if colLength == -1 {
			colLength = len(colsText)
		} else if len(colsText) != colLength {
			return nil, errors.New("not all rows the same length")
		}

		cols := make([]int, colLength)
		for j, colText := range colsText {
			col, error := strconv.Atoi(colText)
			if error != nil {
				return nil, error
			}
			cols[j] = col
		}

		rows[i] = cols
	}
	return &Matrix{rows: rows}, nil
}

func removeEmpty(in []string) (out []string) {
	for _, v := range in {
		if v != "" {
			out = append(out, v)
		}
	}
	return
}

// Rows returns the rows of a matrix
func (matrix Matrix) Rows() (out [][]int) {
	out = make([][]int, len(matrix.rows))
	for i, row := range matrix.rows {
		out[i] = make([]int, len(row))
		copy(out[i], row)
	}
	return
}

// Cols returns the cols of a matrix
func (matrix Matrix) Cols() [][]int {
	if len(matrix.rows) == 0 {
		return [][]int{}
	}
	colCount, rowCount := len(matrix.rows[0]), len(matrix.rows)

	cols := make([][]int, colCount)
	for i := 0; i < colCount; i++ {
		col := make([]int, rowCount)
		for j := 0; j < rowCount; j++ {
			col[j] = matrix.rows[j][i]
		}
		cols[i] = col
	}

	return cols
}

// Set changes the value in the matrix at the specified point
func (matrix *Matrix) Set(row, col, val int) bool {
	if len(matrix.rows) == 0 || row < 0 || col < 0 {
		return false
	}
	colCount, rowCount := len(matrix.rows[0]), len(matrix.rows)
	if row >= rowCount || col >= colCount {
		return false
	}

	matrix.rows[row][col] = val
	return true
}
