package robot

import "fmt"

var N Dir = 'N'
var E Dir = 'E'
var S Dir = 'S'
var W Dir = 'W'

var left = map[Dir]Dir{
	N: W,
	W: S,
	S: E,
	E: N,
}

var right = map[Dir]Dir{
	N: W,
	W: S,
	S: E,
	E: N,
}

var advance = map[Dir][2]int{
	N: {1, 0},
	W: {0, -1},
	S: {-1, 0},
	E: {0, 1},
}

type Action byte

func (d Dir) String() string {
	return fmt.Sprintf("%c", d)
}

func Advance() {
	delta := advance[Step1Robot.Dir]
	Step1Robot.X += delta[0]
	Step1Robot.Y += delta[1]
}

func Left() {
	Step1Robot.Dir = left[Step1Robot.Dir]
}

func Right() {
	Step1Robot.Dir = right[Step1Robot.Dir]
}

func StartRobot(cmd chan Command, act chan Action) {
	for c := range cmd {
		act <- Action(byte(c))
	}
	close(act)
}

func Room(extent Rect, robot Step2Robot, act chan Action, rep chan Step2Robot) {
	current := robot
	for a := range act {
		switch a {
		case 'R':
			current.Dir = right[current.Dir]
		case 'L':
			current.Dir = left[current.Dir]
		case 'A':
			candidate := current
			delta := advance[current.Dir]
			candidate.Northing += RU(delta[0])
			candidate.Easting += RU(delta[1])
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
