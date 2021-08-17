package matrix

import (
	"errors"
	"strconv"
	"strings"
)

type Matrix [][]int
type Pair [2]int

func New(input string) (*Matrix, error) {
	rows := strings.Split(input, "\n")
	m := Matrix{}
	rowLen := -1
	for _, row := range rows {
		cells := strings.Split(row, " ")
		if rowLen == -1 {
			rowLen = len(cells)
		} else if len(cells) != rowLen {
			return nil, errors.New("invalid matrix input")
		}
		r := []int{}
		for _, c := range cells {
			v, err := strconv.Atoi(c)
			if err != nil {
				return nil, errors.New("invalid matrix input")
			}
			r = append(r, v)
		}
		m = append(m, r)
	}
	return &m, nil
}

func max(row []int) int {
	i := 0
	for c := 1; c < len(row); c++ {
		if row[c] > row[i] {
			i = c
		}
	}
	return i
}

func min(rows [][]int, col int) int {
	i := 0
	for r := 1; r < len(rows); r++ {
		if rows[r][col] < rows[i][col] {
			i = r
		}
	}
	return i
}

func (m Matrix) Saddle() []Pair {
	results := []Pair{}
	for r := 0; r < len(m); r++ {
		mx := max(m[r])
		if r == min(m, mx) {
			results = append(results, Pair{r, mx})
		}
	}
	return results
}
