package meetup

import (
	"fmt"
	"time"
)

type WeekSchedule int

const (
	First WeekSchedule = iota
	Second
	Third
	Fourth
	Fifth
	Last
	Teenth
)

func Day(week WeekSchedule, weekday time.Weekday, month time.Month, year int) int {
	day := 1
	weekdays := []int{}
	for {
		date, err := time.Parse("2 January 2006", fmt.Sprintf("%d %s %d", day, month, year))
		if err != nil {
			break
		}
		if date.Weekday() == weekday {
			weekdays = append(weekdays, day)
		}
		day++
	}

	switch week {
	case First:
		return weekdays[0]
	case Second:
		return weekdays[1]
	case Third:
		return weekdays[2]
	case Fourth:
		return weekdays[3]
	case Fifth:
		return weekdays[4]
	case Last:
		return weekdays[len(weekdays)-1]
	case Teenth:
		for _, d := range weekdays {
			if d >= 13 && d <= 19 {
				return d
			}
		}
	}
	panic("no valid date found")
}
