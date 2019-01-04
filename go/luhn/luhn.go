package luhn

import (
	"unicode"
)

// Valid tests if the input is a valid luhn number
func Valid(input string) bool {
	var offset, sum, skip, count int
	if len(input)%2 == 1 {
		offset = 1
	}

	for i, char := range input {
		if char == ' ' {
			skip++
			continue
		}

		if !unicode.IsNumber(char) {
			return false
		}

		toAdd := int(char - '0')

		if (i+offset+skip)%2 == 0 {
			toAdd *= 2
			if toAdd > 9 {
				toAdd -= 9
			}
		}

		sum += toAdd
		count++
	}

	return count > 1 && sum%10 == 0
}
