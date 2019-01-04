package series

// All returns a list of all substrings of s with length n.
func All(n int, s string) []string {
	results := make([]string, len(s)-n+1)
	for i := 0; i < len(results); i++ {
		results[i] = s[i : i+n]
	}
	return results
}

// UnsafeFirst returns the first substring of s with length n.
func UnsafeFirst(n int, s string) string {
	return s[0:n]
}

// First returns the first substring of s with length n, or false if n is invalid
func First(n int, s string) (first string, ok bool) {
	if n <= len(s) {
		first = s[0:n]
		ok = true
	}
	return
}
