package accumulate

// Accumulate is a collect/map/fmap/whatchamacallit implementation
func Accumulate(input []string, operation func(string) string) []string {

	results := make([]string, len(input))

	for i, val := range input {
		results[i] = operation(val)
	}

	return results
}
