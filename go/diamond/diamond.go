package diamond

import (
	"errors"
	"fmt"
)

func spaces(n int) string {
	result := ""
	for i := 0; i < n; i++ {
		result += " "
	}
	return result
}

func row(dist, i int, c byte) string {
	pad := dist - i
	gap := (dist * 2) - (pad * 2) - 1
	cd := c - byte(dist-i)
	if i == 0 {
		return fmt.Sprintf("%s%c%s\n", spaces(pad), cd, spaces(pad))
	} else {
		return fmt.Sprintf("%s%c%s%c%s\n", spaces(pad), cd, spaces(gap), cd, spaces(pad))
	}
}

func Gen(c byte) (string, error) {
	if c < 'A' || c > 'Z' {
		return "", errors.New("invalid character: " + string(c))
	} else if c == 'A' {
		return "A\n", nil
	}

	dist := int(c - 'A')
	result := ""

	for i := 0; i < dist; i++ {
		result += row(dist, i, c)
	}

	result += fmt.Sprintf("%c%s%c\n", c, spaces(dist*2-1), c)

	for i := dist - 1; i >= 0; i-- {
		result += row(dist, i, c)
	}

	return result, nil
}
