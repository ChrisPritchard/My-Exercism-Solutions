package prime

func Nth(n int) (int, bool) {
	if n < 1 {
		return 0, false
	}

	soFar := []int{2}
	c := 3
	for {
		if len(soFar) == n {
			return soFar[len(soFar)-1], true
		}

		isPrime := true
		for _, p := range soFar {
			if c%p == 0 {
				isPrime = false
				break
			}
		}

		if isPrime {
			soFar = append(soFar, c)
		}
		c++
	}
}
