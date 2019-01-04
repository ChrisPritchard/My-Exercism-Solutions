package robotname

import (
	"fmt"
	"math/rand"
)

// Robot represents a robot with a unique name
type Robot struct {
	name string
}

// Name returns the robot's name
func (robot *Robot) Name() string {
	if robot.name == "" {
		robot.name = newName()
	}
	return robot.name
}

// Reset clears and resets the robot's name
func (robot *Robot) Reset() {
	used[robot.name] = false
	robot.name = newName()
}

var used = map[string]bool{}

func newName() string {
	for {
		name := fmt.Sprintf("%c%c%3d", rand.Intn(25)+'A', rand.Intn(25)+'A', rand.Intn(999))
		if used[name] {
			continue
		}

		used[name] = true
		return name
	}
}
