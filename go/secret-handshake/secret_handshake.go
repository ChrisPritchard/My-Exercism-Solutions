package secret

type codeWord struct {
	code uint
	word string
}

var codeWords = []codeWord{
	{1, "wink"},
	{2, "double blink"},
	{4, "close your eyes"},
	{8, "jump"},
}

// Handshake evaluates a code for the hidden signs
func Handshake(code uint) []string {
	result := make([]string, 0)
	for _, val := range codeWords {
		if val.code&code == val.code {
			result = append(result, val.word)
		}
	}
	if 16&code == 16 {
		reversed := make([]string, 0)
		for i := len(result) - 1; i >= 0; i-- {
			reversed = append(reversed, result[i])
		}
		result = reversed
	}
	return result
}
