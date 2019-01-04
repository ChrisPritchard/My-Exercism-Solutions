package wordcount

import "strings"

// Frequency returns the count of each word in some input
type Frequency map[string]int

// WordCount returns the count of each word in the input string
func WordCount(input string) Frequency {
	input = strings.ToLower(input)

	replacements := []string{",", ":", ".", "\n", "!", "&", "@", "$", "%", "^"}
	for _, v := range replacements {
		input = strings.Replace(input, v, " ", -1)
	}

	words := strings.Split(input, " ")

	result := Frequency{}
	for _, v := range words {
		if v != "" {
			if v[0] == '\'' {
				v = v[1 : len(v)-1]
			}
			result[v] = result[v] + 1
		}
	}
	return result
}
