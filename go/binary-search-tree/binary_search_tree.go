package binarysearchtree

type SearchTreeData struct {
	left  *SearchTreeData
	data  int
	right *SearchTreeData
}

func Bst(n int) *SearchTreeData {
	return &SearchTreeData{nil, n, nil}
}

func (s *SearchTreeData) Insert(v int) {
	if v <= s.data {
		if s.left == nil {
			s.left = Bst(v)
		} else {
			s.left.Insert(v)
		}
	} else {
		if s.right == nil {
			s.right = Bst(v)
		} else {
			s.right.Insert(v)
		}
	}
}

func (s *SearchTreeData) MapString(m func(int) string) []string {
	r := []string{m(s.data)}
	if s.left != nil {
		r = append(s.left.MapString(m), r...)
	}
	if s.right != nil {
		r = append(r, s.right.MapString(m)...)
	}
	return r
}

func (s *SearchTreeData) MapInt(m func(int) int) []int {
	r := []int{m(s.data)}
	if s.left != nil {
		r = append(s.left.MapInt(m), r...)
	}
	if s.right != nil {
		r = append(r, s.right.MapInt(m)...)
	}
	return r
}
