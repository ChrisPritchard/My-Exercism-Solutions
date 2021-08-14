package strain

type Ints []int
type Lists [][]int
type Strings []string

func (o Ints) Keep(p func(int) bool) Ints {
	if o == nil {
		return nil
	}
	res := Ints{}
	for _, v := range o {
		if p(v) {
			res = append(res, v)
		}
	}
	return res
}

func (o Ints) Discard(p func(int) bool) Ints {
	if o == nil {
		return nil
	}
	res := Ints{}
	for _, v := range o {
		if !p(v) {
			res = append(res, v)
		}
	}
	return res
}

func (o Lists) Keep(p func([]int) bool) Lists {
	if o == nil {
		return nil
	}
	res := Lists{}
	for _, v := range o {
		if p(v) {
			res = append(res, v)
		}
	}
	return res
}

func (o Strings) Keep(p func(string) bool) Strings {
	if o == nil {
		return nil
	}
	res := Strings{}
	for _, v := range o {
		if p(v) {
			res = append(res, v)
		}
	}
	return res
}
