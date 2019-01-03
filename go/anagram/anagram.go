package anagram

import (
	"reflect"
	"sort"
	"strings"
)

// Detect returns all candidates that are anagrams of the subject
func Detect(subject string, candidates []string) []string {
	source := sorted(strings.ToLower(subject))
	result := []string{}
	for _, v := range candidates {
		if strings.ToLower(v) != strings.ToLower(subject) {
			candidate := sorted(strings.ToLower(v))
			if reflect.DeepEqual(candidate, source) {
				result = append(result, v)
			}
		}
	}
	return result
}

func sorted(subject string) []rune {
	asRunes := []rune(subject)
	sort.Slice(asRunes, func(i, j int) bool {
		return asRunes[i] < asRunes[j]
	})
	return asRunes
}
