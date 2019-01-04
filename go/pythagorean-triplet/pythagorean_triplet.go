package pythagorean

// Triplet holds a potential pythagorean triplet
type Triplet [3]int

// Range returns a list of all Pythagorean triplets with sides in the range min to max inclusive.
func Range(min, max int) []Triplet {
	results := make([]Triplet, 0)
	for a := min; a <= max; a++ {
		for b := a + 1; b <= max; b++ {
			for c := b + 1; c <= max; c++ {
				if a*a+b*b == c*c {
					results = append(results, Triplet{a, b, c})
				}
			}
		}
	}
	return results
}

// Sum returns a list of all Pythagorean triplets where the sum a+b+c (the perimeter) is equal to p.
func Sum(p int) []Triplet {
	results := make([]Triplet, 0)
	for _, val := range Range(1, p) {
		if val[0]+val[1]+val[2] == p {
			results = append(results, val)
		}
	}
	return results
}
