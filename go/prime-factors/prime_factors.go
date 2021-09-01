package prime

func Factors(n int64) []int64 {
	var i int64 = 2
	for i <= n {
		if n%i == 0 {
			return append([]int64{i}, Factors(n/i)...)
		}
		i++
	}
	return []int64{}
}
