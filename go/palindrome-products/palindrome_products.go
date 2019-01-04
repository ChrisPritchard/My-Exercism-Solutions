package palindrome

import "errors"

// Product represents a palindromic product, with its factorisations
type Product struct {
	Product        int      // palindromic, of course
	Factorizations [][2]int //list of all possible two-factor factorizations of Product, within given limits, in order
}

// Products returns the min and max palindrome products in a given range
func Products(fmin, fmax int) (pmin, pmax Product, error error) {
	if fmin > fmax {
		error = errors.New("fmin > fmax")
		return
	}

	products := make(map[int][][2]int)

	for i := fmin; i <= fmax; i++ {
		for j := i; j <= fmax; j++ {
			product := i * j
			if !isPalindrome(product) {
				continue
			}
			factors := [2]int{i, j}

			if found, ok := products[product]; ok {
				products[product] = append(found, factors)
			} else {
				products[product] = [][2]int{factors}
			}
		}
	}

	if len(products) == 0 {
		error = errors.New("no palindromes")
		return
	}

	for product, factors := range products {
		if pmin.Product > product || pmin.Product == 0 {
			pmin = Product{product, factors}
		}

		if pmax.Product < product || pmax.Product == 0 {
			pmax = Product{product, factors}
		}
	}

	return
}

func isPalindrome(n int) bool {
	reverse := 0
	original := n

	for {
		reverse = reverse*10 + n%10
		n /= 10
		if n == 0 {
			break
		}
	}

	return reverse == original
}
