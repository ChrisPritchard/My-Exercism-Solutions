package summultiples

// SumMultiples sums up all numbers below the limit that are divisible by the given devisors
func SumMultiples(limit int, divisors ...int) int {
	sum := 0
	for i := 1; i < limit; i++ {
		for _, divisor := range divisors {
			if i%divisor == 0 {
				sum += i
				break
			}
		}
	}
	return sum
}
