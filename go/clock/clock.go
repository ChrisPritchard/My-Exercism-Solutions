package clock

import (
	"fmt"
)

// Clock contains hours and minutes
type Clock int

// New returns a new clock
func New(hours, minutes int) Clock {
	totalMinutes := (hours*60 + minutes) % 1440
	if totalMinutes < 0 {
		return Clock(totalMinutes + 1440)
	}
	return Clock(totalMinutes)
}

func (clock Clock) String() string {
	hours := clock / 60
	minutes := clock % 60
	return fmt.Sprintf("%02d:%02d", hours, minutes)
}

// Add returns a new clock with the added minutes
func (clock Clock) Add(minutes int) Clock {
	return New(0, int(clock)+minutes)
}

// Subtract returns a new clock with the subtracted minutes
func (clock Clock) Subtract(minutes int) Clock {
	return clock.Add(-minutes)
}
