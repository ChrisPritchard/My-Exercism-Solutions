// Package hamming contains a method for calculating hamming distance.
package hamming

import "errors"

// Distance returns the Hamming distance between two strands.
func Distance(a, b string) (int, error) {
	if len(a) != len(b) {
		return 0, errors.New("a and b are of different lengths")
	}

	distance := 0
	for i := 0; i < len(a); i++ {
		if a[i] != b[i] {
			distance++
		}
	}

	return distance, nil
}
