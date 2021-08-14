package piglatin

import "strings"

var vowels = []string{"a", "e", "i", "o", "u", "xr", "yt"}

func Sentence(source string) string {
	if strings.Contains(source, " ") {
		words := strings.Split(source, " ")
		result := ""
		for _, w := range words {
			result += " " + Sentence(w)
		}
		return strings.Trim(result, " ")
	}

	for _, v := range vowels {
		if strings.HasPrefix(source, v) {
			return source + "ay"
		}
	}

	special := strings.Index(source, "qu")
	if special != -1 {
		return source[special+2:] + source[:special] + "quay"
	}

	firstVowel := len(source)
	for _, v := range vowels {
		i := strings.Index(source, v)
		if i != -1 && i < firstVowel {
			firstVowel = i
		}
	}
	if firstVowel == len(source) {
		firstVowel = 0
	}

	consonantSound := source
	if firstVowel != 0 {
		consonantSound = source[:firstVowel]
	}

	if len(consonantSound) > 1 {
		consonantSound = strings.TrimRight(consonantSound, "y")
	}

	return source[len(consonantSound):] + consonantSound + "ay"
}
