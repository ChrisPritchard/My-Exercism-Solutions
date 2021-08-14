package phonenumber

import (
	"errors"
	"fmt"
	"strings"
)

func Number(source string) (string, error) {
	numbersOnly := ""
	for _, c := range source {
		if strings.ContainsAny(string(c), " .-()+") {
			continue
		}
		if c < '0' || c > '9' {
			return "", errors.New("Invalid")
		}
		numbersOnly += string(c)
	}

	if len(numbersOnly) < 10 || len(numbersOnly) > 11 {
		return "", errors.New("Invalid")
	}

	if len(numbersOnly) == 11 && numbersOnly[0] != '1' {
		return "", errors.New("Invalid")
	}

	withoutCountryCode := numbersOnly
	if len(numbersOnly) == 11 {
		withoutCountryCode = numbersOnly[1:]
	}

	if withoutCountryCode[0] < '2' || withoutCountryCode[3] < '2' {
		return "", errors.New("Invalid")
	}

	return withoutCountryCode, nil
}

func Format(source string) (string, error) {
	clean, err := Number(source)
	if err != nil {
		return "", err
	}
	return fmt.Sprintf("(%s) %s-%s", clean[:3], clean[3:6], clean[6:]), nil
}

func AreaCode(source string) (string, error) {
	clean, err := Number(source)
	if err != nil {
		return "", err
	}
	return clean[:3], nil
}
