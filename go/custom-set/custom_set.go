package stringset

type Set struct {
	inner map[string]interface{}
}

func New() Set {
	return Set{inner: make(map[string]interface{})}
}

func NewFromSlice(slice []string) Set {
	result := New()
	for _, v := range slice {
		result.Add(v)
	}
	return result
}

func (s Set) String() string {
	if len(s.inner) == 0 {
		return "{}"
	}
	result := "{"
	for k := range s.inner {
		result += "\"" + k + "\", "
	}
	return result[:len(result)-2] + "}"
}

func (s Set) IsEmpty() bool {
	return len(s.inner) == 0
}

func (s Set) Has(elem string) bool {
	_, exists := s.inner[elem]
	return exists
}

func (s *Set) Add(elem string) {
	s.inner[elem] = nil
}

func Equal(a, b Set) bool {
	if len(a.inner) != len(b.inner) {
		return false
	}
	for k := range a.inner {
		if !b.Has(k) {
			return false
		}
	}
	return true
}

// Subset checks if a subset of b
func Subset(a, b Set) bool {
	if len(a.inner) > len(b.inner) {
		return false
	}
	for k := range a.inner {
		if !b.Has(k) {
			return false
		}
	}
	return true
}

func Disjoint(a, b Set) bool {
	for k := range a.inner {
		if b.Has(k) {
			return false
		}
	}
	for k := range b.inner {
		if a.Has(k) {
			return false
		}
	}
	return true
}

// Intersection returns a set of all shared elements
func Intersection(a, b Set) Set {
	result := New()
	for k := range a.inner {
		if b.Has(k) {
			result.Add(k)
		}
	}
	for k := range b.inner {
		if a.Has(k) {
			result.Add(k)
		}
	}
	return result
}

// Difference (or Complement) of a set is a set of all elements that are only in the first set
func Difference(a, b Set) Set {
	result := New()
	for k := range a.inner {
		if !b.Has(k) {
			result.Add(k)
		}
	}
	return result
}

// Union returns a set of all elements in either set
func Union(a, b Set) Set {
	result := New()
	for k := range a.inner {
		result.Add(k)
	}
	for k := range b.inner {
		result.Add(k)
	}
	return result
}
