package atbash

import "strings"

func Atbash(s string) string {
	result := ""
	n := 0
	for _, c := range s {
		if c >= 'A' && c <= 'Z' {
			c = (c - 'A') + 'a'
		}
		if c < '0' || c > '9' {
			if c < 'a' || c > 'z' {
				continue
			}
			c = 'z' - (c - 'a')
		}
		result += string(c)
		n++
		if n%5 == 0 {
			result += " "
		}
	}
	return strings.Trim(result, " ")
}
