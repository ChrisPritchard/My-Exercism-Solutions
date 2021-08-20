package kindergarten

import (
	"errors"
	"sort"
	"strings"
)

type Garden map[string][]string

func NewGarden(diagram string, children []string) (*Garden, error) {
	rows := strings.Split(diagram, "\n")
	if len(rows) != 3 || len(rows[1]) != len(rows[2]) || len(rows[1]) != len(children)*2 {
		return nil, errors.New("invalid input")
	}

	sorted := append([]string{}, children...)
	sort.Slice(sorted, func(a, b int) bool {
		return sorted[a] < sorted[b]
	})
	result := make(Garden)

	for i, c := range sorted {
		if _, exists := result[c]; exists {
			return nil, errors.New("invalid input")
		}
		result[c] = []string{}
		for _, p := range rows[1][i*2:i*2+2] + rows[2][i*2:i*2+2] {
			switch p {
			case 'G':
				result[c] = append(result[c], "grass")
			case 'C':
				result[c] = append(result[c], "clover")
			case 'R':
				result[c] = append(result[c], "radishes")
			case 'V':
				result[c] = append(result[c], "violets")
			default:
				return nil, errors.New("invalid input")
			}
		}
	}

	return &result, nil
}

func (g *Garden) Plants(child string) ([]string, bool) {
	plants, exists := (*g)[child]
	return plants, exists
}
