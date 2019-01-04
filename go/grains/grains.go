package grains

import (
	"errors"
)

// Square returns the number of grains on a given square
func Square(index int) (uint64, error) {
	if index < 1 || index > 64 {
		return 0, errors.New("Invalid index, must be between 1 and 64")
	}
	power := uint64(index - 1)
	return 1 << power, nil
}

// Total returns the total number of grains on an entire board
func Total() uint64 {
	var sum uint64
	for i := 1; i <= 64; i++ {
		count, error := Square(i)
		if error != nil {
			panic(error)
		}
		sum += count
	}
	return sum
}
