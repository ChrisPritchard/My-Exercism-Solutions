package reverse

import "strings"

// String reverses an input string
func String(input string) string {
	runes := []rune(input)
	var builder strings.Builder
	for i := len(runes) - 1; i >= 0; i-- {
		builder.WriteRune(runes[i])
	}
	return builder.String()
}
