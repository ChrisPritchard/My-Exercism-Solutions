package minesweeper

import "errors"

var ErrInvalidBoard error = errors.New("invalid board")

func checkValid(b Board) error {
	lx := len(b[0])
	for y := 0; y < len(b); y++ {
		if len(b[y]) != lx {
			return ErrInvalidBoard
		}
		for x := 0; x < len(b[0]); x++ {
			c := b[y][x]
			ey := y == 0 || y == len(b)-1
			ex := x == 0 || x == len(b[0])-1
			if ex && ey {
				if c != '+' {
					return ErrInvalidBoard
				}
			} else if ex || ey {
				if (ey && c != '-') || (ex && c != '|') {
					return ErrInvalidBoard
				}
			} else {
				if c != ' ' && c != '*' {
					return ErrInvalidBoard
				}
			}
		}
	}

	return nil
}

func (b Board) count(x, y int) byte {
	if b[y][x] == '*' {
		return '*'
	}
	c := 0
	for dx := x - 1; dx <= x+1; dx++ {
		for dy := y - 1; dy <= y+1; dy++ {
			if b[dy][dx] == '*' {
				c++
			}
		}
	}
	if c == 0 {
		return ' '
	}
	return byte('0' + c)
}

func (b *Board) Count() error {
	err := checkValid(*b)
	if err != nil {
		return err
	}
	for x := 1; x < len((*b)[0])-1; x++ {
		for y := 1; y < len((*b))-1; y++ {
			(*b)[y][x] = b.count(x, y)
		}
	}
	return nil
}
