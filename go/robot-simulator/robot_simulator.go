package robot

import "fmt"

var N Dir = 'N'
var E Dir = 'E'
var S Dir = 'S'
var W Dir = 'W'

func init() {
	Step1Robot.Dir = N
}

var left = map[Dir]Dir{
	N: W,
	W: S,
	S: E,
	E: N,
}

var right = map[Dir]Dir{
	N: E,
	W: N,
	S: W,
	E: S,
}

var advance = map[Dir][2]int{
	N: {0, 1},
	W: {-1, 0},
	S: {0, -1},
	E: {1, 0},
}

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

type Action byte

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
			candidate.Easting += RU(delta[0])
			candidate.Northing += RU(delta[1])
			if candidate.Easting < extent.Min.Easting || candidate.Easting > extent.Max.Easting {
				continue
			}
			if candidate.Northing < extent.Min.Northing || candidate.Northing > extent.Max.Northing {
				continue
			}
			current = candidate
		}
	}
	rep <- current
}

type Action3 struct {
	name   string
	action byte
}

func StartRobot3(name, script string, action chan Action3, log chan string) {
	if len(name) == 0 {
		log <- "missing robot name, quitting"
		action <- Action3{name, 0}
		return
	}
	for _, r := range script {
		if r != 'L' && r != 'R' && r != 'A' {
			log <- "undefined action: " + string(r)
			break
		} else {
			action <- Action3{name, byte(r)}
		}
	}
	action <- Action3{name, 0}
}

func Room3(extent Rect, robots []Step3Robot, action chan Action3, report chan []Step3Robot, log chan string) {
	currentAll := make(map[string]Step3Robot)
	finished := make(map[string]bool)
	for _, r := range robots {
		if _, exists := currentAll[r.Name]; exists {
			log <- "duplicate robot names: " + r.Name
			report <- robots
			return
		}
		for _, e := range currentAll {
			if e.Pos == r.Pos {
				log <- fmt.Sprintf("robot %s cannot start at %v as robot %s is already there", r.Name, r.Pos, e.Name)
				report <- robots
				return
			}
		}
		if r.Easting < extent.Min.Easting || r.Easting > extent.Max.Easting || r.Northing < extent.Min.Northing || r.Northing > extent.Max.Northing {
			log <- fmt.Sprintf("robot %s tried to start outside the room", r.Name)
			report <- robots
			return
		}
		currentAll[r.Name] = r
	}

	for {
		if len(finished) == len(currentAll) {
			break
		}
		a := <-action

		current, exists := currentAll[a.name]
		if !exists {
			log <- "unknown robot: " + a.name
			report <- robots
			return
		}
		if finished[a.name] {
			continue
		}
		switch a.action {
		case 0:
			finished[a.name] = true
			continue
		case 'R':
			current.Dir = right[current.Dir]
		case 'L':
			current.Dir = left[current.Dir]
		case 'A':
			candidate := current
			delta := advance[current.Dir]
			candidate.Easting += RU(delta[0])
			candidate.Northing += RU(delta[1])
			valid := true
			for _, e := range currentAll {
				if e.Pos == candidate.Pos {
					log <- fmt.Sprintf("robot %s cannot move to %v as robot %s is already there", candidate.Name, candidate.Pos, e.Name)
					valid = false
					break
				}
			}
			if candidate.Easting < extent.Min.Easting || candidate.Easting > extent.Max.Easting || candidate.Northing < extent.Min.Northing || candidate.Northing > extent.Max.Northing {
				log <- fmt.Sprintf("robot %s tried to move outside the room", candidate.Name)
				valid = false
			}
			if valid {
				current = candidate
			}
		}
		currentAll[a.name] = current
	}

	result := []Step3Robot{}
	for _, r := range currentAll {
		result = append(result, r)
	}
	report <- result
}
