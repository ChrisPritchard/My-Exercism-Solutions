package robot

import "fmt"

var N Dir = 'N'
var E Dir = 'E'
var S Dir = 'S'
var W Dir = 'W'

type Action byte

func (d Dir) String() string {
	return fmt.Sprintf("%c", d)
}

func Advance() {
	switch Step1Robot.Dir {
	case N:
		Step1Robot.Y++
	case S:
		Step1Robot.Y--
	case E:
		Step1Robot.X++
	case W:
		Step1Robot.X--
	}
}

func Right() {
	switch Step1Robot.Dir {
	case N:
		Step1Robot.Dir = E
	case S:
		Step1Robot.Dir = W
	case E:
		Step1Robot.Dir = S
	case W:
		Step1Robot.Dir = N
	}
}

func Left() {
	switch Step1Robot.Dir {
	case N:
		Step1Robot.Dir = W
	case S:
		Step1Robot.Dir = E
	case E:
		Step1Robot.Dir = N
	case W:
		Step1Robot.Dir = S
	}
}

func StartRobot(cmd chan Command, act chan Action) {
	for c := range cmd {
		switch c {
		case 'R':
			act <- 'R'
		case 'L':
			act <- 'L'
		case 'A':
			act <- 'A'
		}
	}
	close(act)
}

func Room(extent Rect, robot Step2Robot, act chan Action, rep chan Step2Robot) {
	current := robot
	for a := range act {
		switch a {
		case 'R':
			switch current.Dir {
			case N:
				current.Dir = E
			case S:
				current.Dir = W
			case E:
				current.Dir = S
			case W:
				current.Dir = N
			}
		case 'L':
			switch current.Dir {
			case N:
				current.Dir = W
			case S:
				current.Dir = E
			case E:
				current.Dir = N
			case W:
				current.Dir = S
			}
		case 'A':
			candidate := current
			switch candidate.Dir {
			case N:
				candidate.Northing++
			case S:
				candidate.Northing--
			case E:
				candidate.Easting++
			case W:
				candidate.Easting--
			}
			if candidate.Easting < extent.Min.Easting || candidate.Easting > extent.Max.Easting {
				continue
			}
			if candidate.Northing < extent.Min.Northing || candidate.Northing > extent.Max.Northing {
				continue
			}
			current = candidate
		}
	}
	rep <- robot
}
