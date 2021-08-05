package railfence

import "strings"

func Encode(text string, rails int) string {
	text = strings.ReplaceAll(text, " ", "")
	lines := make([]string, rails)
	r, d := 0, 1
	for _, c := range text {
		lines[r] += string(c)
		if d == 1 && r == len(lines)-1 {
			d = -1
		} else if d == -1 && r == 0 {
			d = 1
		}
		r += d
	}
	result := ""
	for _, line := range lines {
		result += line
	}
	return result
}

func Decode(cipher string, rails int) string {
	lines := make([][]int, rails)
	r, d := 0, 1
	for i := 0; i < len(cipher); i++ {
		lines[r] = append(lines[r], i)
		if d == 1 && r == len(lines)-1 {
			d = -1
		} else if d == -1 && r == 0 {
			d = 1
		}
		r += d
	}
	result := make([]byte, len(cipher))
	i := 0
	for _, line := range lines {
		for _, index := range line {
			result[index] = cipher[i]
			i++
		}
	}
	return string(result)
}
