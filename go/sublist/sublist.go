package sublist

// Relation represents the relationship between two lists
type Relation string

// Sublist returns whether 'a' is a sublist, superlist, equal to or unequal to 'b'
func Sublist(a, b []int) Relation {
	if len(a) == len(b) && contains(a, b) {
		return "equal"
	}

	if len(a) < len(b) && contains(b, a) {
		return "sublist"
	}

	if len(b) < len(a) && contains(a, b) {
		return "superlist"
	}

	return "unequal"
}

func contains(parent, child []int) bool {
	for i := 0; i < len(parent)-len(child)+1; i++ {
		found := true
		for j := 0; j < len(child); j++ {
			if parent[i+j] != child[j] {
				found = false
				break
			}
		}
		if found {
			return true
		}
	}
	return false
}
