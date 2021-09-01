package wordy

import (
	"strconv"
	"strings"
)

func Answer(question string) (int, bool) {
	if !strings.HasPrefix(question, "What is ") || !strings.HasSuffix(question, "?") {
		return 0, false
	}

	body := question[len("What is ") : len(question)-1]
	body = strings.ReplaceAll(body, "divided by", "divided_by")
	body = strings.ReplaceAll(body, "multiplied by", "multiplied_by")
	parts := strings.Split(body, " ")

	if len(parts) == 0 {
		return 0, false
	}

	result, err := strconv.Atoi(parts[0])
	if err != nil {
		return 0, false
	}
	parts = parts[1:]

	isOp := false
	op := func(a, b int) int { return 0 }
	for len(parts) > 0 {
		converted, err := strconv.Atoi(parts[0])
		if err == nil && isOp {
			result = op(result, converted)
			isOp = false
			parts = parts[1:]
			continue
		}

		if isOp {
			return 0, false
		}
		isOp = true

		switch parts[0] {
		case "plus":
			op = func(a, b int) int { return a + b }
		case "minus":
			op = func(a, b int) int { return a - b }
		case "divided_by":
			op = func(a, b int) int { return a / b }
		case "multiplied_by":
			op = func(a, b int) int { return a * b }
		default:
			return 0, false
		}

		parts = parts[1:]
	}

	if isOp {
		return 0, false
	}
	return result, true
}
