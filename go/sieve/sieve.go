package sieve

// Sieve returns all primes up to the limit
func Sieve(limit int) []int {
	nonprimes := map[int]bool{}
	result := []int{}
	for i := 2; i <= limit; i++ {
		if nonprimes[i] {
			continue
		}
		result = append(result, i)
		for j := i * 2; j <= limit; j += i {
			nonprimes[j] = true
		}
	}
	return result
}
