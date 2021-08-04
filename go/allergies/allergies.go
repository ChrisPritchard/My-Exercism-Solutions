package allergies

import "sort"

var allergins = map[string]uint{
	"eggs":         1,
	"peanuts":      2,
	"shellfish":    4,
	"strawberries": 8,
	"tomatoes":     16,
	"chocolate":    32,
	"pollen":       64,
	"cats":         128,
}

func Allergies(score uint) []string {
	result := []string{}
	for k, v := range allergins {
		if score&v == v {
			result = append(result, k)
		}
	}
	sort.Slice(result, func(i, j int) bool {
		return allergins[result[i]] < allergins[result[j]]
	})
	return result
}

func AllergicTo(score uint, substance string) bool {
	return score&allergins[substance] == allergins[substance]
}
