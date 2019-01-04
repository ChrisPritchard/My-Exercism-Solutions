package raindrops

import "strconv"

type rainsound struct {
	factor int
	sound  string
}

var sounds = []rainsound{
	{3, "Pling"},
	{5, "Plang"},
	{7, "Plong"},
}

// Convert returns rainsounds for a given input value
func Convert(input int) string {
	result := ""
	for _, val := range sounds {
		if input%val.factor == 0 {
			result += val.sound
		}
	}

	if len(result) == 0 {
		return strconv.Itoa(input)
	}

	return result
}
