package beer

import (
	"fmt"
)

var patternN = "%d bottles of beer on the wall, %d bottles of beer.\nTake one down and pass it around, %d bottles of beer on the wall.\n"
var pattern2 = "2 bottles of beer on the wall, 2 bottles of beer.\nTake one down and pass it around, 1 bottle of beer on the wall.\n"
var pattern1 = "1 bottle of beer on the wall, 1 bottle of beer.\nTake it down and pass it around, no more bottles of beer on the wall.\n"
var pattern0 = "No more bottles of beer on the wall, no more bottles of beer.\nGo to the store and buy some more, 99 bottles of beer on the wall.\n"

func Verse(n int) (string, error) {
	if n < 0 || n > 99 {
		return "", fmt.Errorf("invalid verse number: %d", n)
	}

	switch n {
	case 0:
		return pattern0, nil
	case 1:
		return pattern1, nil
	case 2:
		return pattern2, nil
	default:
		return fmt.Sprintf(patternN, n, n, n-1), nil
	}
}

func Verses(upperBound, lowerBound int) (string, error) {
	if upperBound < lowerBound {
		return "", fmt.Errorf("invalid upperbound %d must be higher than lowerbound %d", upperBound, lowerBound)
	}

	result := ""
	for i := upperBound; i >= lowerBound; i-- {
		verse, err := Verse(i)
		if err != nil {
			return "", err
		}
		result += verse + "\n"
	}

	return result, nil
}

func Song() string {
	result, _ := Verses(99, 0)
	return result
}
