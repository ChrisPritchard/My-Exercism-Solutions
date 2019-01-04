package cryptosquare

import (
	"math"
	"strings"
	"unicode"
)

// Encode produces a cypher using the crypto square algorithm
func Encode(raw string) string {
	var cleaner strings.Builder
	for _, char := range raw {
		if unicode.IsNumber(char) || unicode.IsLetter(char) {
			cleaner.WriteRune(unicode.ToLower(char))
		}
	}
	clean := []rune(cleaner.String())
	length := len(clean)

	root := math.Sqrt(float64(length))
	cols := int(math.Ceil(root))
	rows := cols - 1
	if rows*cols < length {
		rows++
	}

	var cypher strings.Builder
	for c := 0; c < cols; c++ {
		for r := 0; r < rows; r++ {
			index := r*cols + c
			if index >= length {
				cypher.WriteRune(' ')
			} else {
				cypher.WriteRune(clean[index])
			}
		}
		if c != cols-1 {
			cypher.WriteRune(' ')
		}
	}

	return cypher.String()
}
