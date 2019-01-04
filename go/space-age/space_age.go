package space

const earthSeconds = 31557600.0

// Planet is a named string type.
type Planet string

// Age returns the equivalent age on a given planet in that planet's years.
func Age(seconds float64, planet Planet) float64 {
	switch planet {
	case "Mercury":
		return seconds / earthSeconds / 0.2408467
	case "Venus":
		return seconds / earthSeconds / 0.61519726
	case "Mars":
		return seconds / earthSeconds / 1.8808158
	case "Jupiter":
		return seconds / earthSeconds / 11.862615
	case "Saturn":
		return seconds / earthSeconds / 29.447498
	case "Uranus":
		return seconds / earthSeconds / 84.016846
	case "Neptune":
		return seconds / earthSeconds / 164.79132
	default:
		return seconds / earthSeconds
	}
}
