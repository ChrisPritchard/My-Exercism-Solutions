package queenattack

import (
	"errors"
	"math"
)

// CanQueenAttack tests if a queen chess piece
// can attack from one position to another
func CanQueenAttack(pos1, pos2 string) (bool, error) {
	if pos1 == pos2 {
		return false, errors.New("input coords the same")
	}
	x1, y1, x2, y2 :=
		[]rune(pos1)[0]-'a', []rune(pos1)[1]-'1',
		[]rune(pos2)[0]-'a', []rune(pos2)[1]-'1'
	for _, n := range []rune{x1, x2, y1, y2} {
		if n < 0 || n > 7 {
			return false, errors.New("invalid input coords")
		}
	}
	if x1 == x2 || y1 == y2 || math.Abs(float64(x1-x2)) == math.Abs(float64(y1-y2)) {
		return true, nil
	}
	return false, nil
}
