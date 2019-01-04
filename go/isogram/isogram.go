package isogram

import (
	"unicode"
)

// IsIsogram tests if a given input string has no repeating letters
func IsIsogram(input string) bool {

	found := make(map[rune]bool)

	for _, char := range input {
		if !unicode.IsLetter(char) {
			continue
		}

		char = unicode.ToUpper(char)
		if found[char] {
			return false
		}

		found[char] = true
	}

	return true
}
