package pangram

import (
	"unicode"
)

// IsPangram tests if the input string contains the entire alphabet
func IsPangram(input string) bool {
	alpha := make(map[rune]bool)
	for _, char := range input {
		var sut = unicode.ToLower(char)
		if sut >= 'a' && sut <= 'z' {
			alpha[sut] = true
		}
	}
	return len(alpha) == 26
}
