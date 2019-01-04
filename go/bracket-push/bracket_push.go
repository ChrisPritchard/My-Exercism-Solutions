package brackets

// Bracket tests if the string contains matching brackets
func Bracket(s string) (bool, error) {
	input := []rune(s)
	stack := []rune{}

	stackLen := 0
	for _, val := range input {
		stackLen = len(stack)
		switch val {
		case '[', '{', '(':
			stack = append(stack, val)
		case ']':
			if stackLen > 0 && stack[stackLen-1] == '[' {
				stack = stack[0 : stackLen-1]
			} else {
				return false, nil
			}
		case '}':
			if stackLen > 0 && stack[stackLen-1] == '{' {
				stack = stack[0 : stackLen-1]
			} else {
				return false, nil
			}
		case ')':
			if stackLen > 0 && stack[stackLen-1] == '(' {
				stack = stack[0 : stackLen-1]
			} else {
				return false, nil
			}
		default:
			continue
		}
	}

	return len(stack) == 0, nil
}
