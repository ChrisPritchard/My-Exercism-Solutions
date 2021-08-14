package armstrong

import (
	"math"
	"strconv"
)

func IsNumber(n int) bool {
	sum := 0
	s := strconv.Itoa(n)
	for _, c := range s {
		r := math.Pow(float64(c-'0'), float64(len(s)))
		sum += int(r)
	}
	return sum == n
}
