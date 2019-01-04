package letter

// FreqMap represents a char-count map
type FreqMap map[rune]int

// Frequency counts the incident of letters in a given snippet
func Frequency(s string) FreqMap {
	m := FreqMap{}
	for _, r := range s {
		m[r]++
	}
	return m
}

// ConcurrentFrequency runs concurrent goroutines over an array of snippets
func ConcurrentFrequency(snippets []string) FreqMap {
	channel := make(chan FreqMap)
	for _, snippet := range snippets {
		go func(s string) {
			channel <- Frequency(s)
		}(snippet)
	}

	resultMap := FreqMap{}

	for i := 0; i < len(snippets); i++ {
		localMap := <-channel
		for key, val := range localMap {
			resultMap[key] += val
		}
	}

	return resultMap
}
