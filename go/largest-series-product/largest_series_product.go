package lsproduct

import "errors"

// LargestSeriesProduct returns the largest product of substrings in the input of the given length
func LargestSeriesProduct(digits string, span int) (result int, e error) {
	if span > len(digits) {
		return 0, errors.New("span must be smaller than string length")
	}
	if span < 0 {
		return 0, errors.New("span must be greater than zero")
	}

	var max int
	for i := 0; i <= len(digits)-span; i++ {
		substrg := []rune(digits[i : i+span])
		product := 1
		for j := 0; j < len(substrg); j++ {
			multiplier := substrg[j] - '0'
			if multiplier < 0 || multiplier > 9 {
				return 0, errors.New("digits input must only contain digits")
			}
			product *= int(multiplier)
		}
		if product > max {
			max = product
		}
	}

	return max, nil
}
