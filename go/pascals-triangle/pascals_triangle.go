package pascal

// Triangle returns the pascal triangle up to the specific level
func Triangle(n int) (result [][]int) {
	result = make([][]int, n)

	for r := 0; r < n; r++ {
		if r == 0 {
			result[r] = []int{1}
			continue
		}
		result[r] = make([]int, r+1)
		for c := 0; c <= r; c++ {
			if c == 0 || c == r {
				result[r][c] = 1
			} else {
				result[r][c] = result[r-1][c-1] + result[r-1][c]
			}
		}
	}

	return
}
