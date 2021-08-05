package romannumerals

import (
	"errors"
	"strconv"
)

func translate(n byte, one, five, ten string) string {
	switch n {
	case '1':
		return one
	case '2':
		return one + one
	case '3':
		return one + one + one
	case '4':
		return one + five
	case '5':
		return five
	case '6':
		return five + one
	case '7':
		return five + one + one
	case '8':
		return five + one + one + one
	case '9':
		return one + ten
	}
	return ""
}

func ToRomanNumeral(arabic int) (string, error) {
	if arabic < 1 || arabic > 3000 {
		return "", errors.New("only numbers between 1 and 3000 inclusive are supported")
	}
	source := strconv.Itoa(arabic)
	result := ""
	if len(source) == 4 {
		result += translate(source[0], "M", "", "")
		source = source[1:]
	}
	if len(source) == 3 {
		result += translate(source[0], "C", "D", "M")
		source = source[1:]
	}

	if len(source) == 2 {
		result += translate(source[0], "X", "L", "C")
		source = source[1:]
	}
	if len(source) == 1 {
		result += translate(source[0], "I", "V", "X")
	}
	return result, nil
}
