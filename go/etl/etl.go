package etl

import "strings"

// Transform changes the old scoring map format into the new format
func Transform(input map[int][]string) map[string]int {
	result := make(map[string]int)
	for key, chars := range input {
		for _, char := range chars {
			result[strings.ToLower(char)] = key
		}
	}
	return result
}
