package transpose

func Transpose(input []string) []string {
	colLength := 0
	for i := len(input) - 1; i >= 0; i-- {
		for len(input[i]) < colLength {
			input[i] += " "
		}
		colLength = len(input[i])
	}
	output := make([]string, colLength)
	for i := 0; i < len(input); i++ {
		for c := 0; c < colLength; c++ {
			if c < len(input[i]) {
				output[c] += string(input[i][c])
			}
		}
	}
	return output
}
