package react

// idea, maintain a set of dependents
// when ever a input cell or compute cell value changes
// find all dependents, and update them
// dependents can only be compute cells by definition
//

type MyReactor struct {
}

func New() *MyReactor {
	return &MyReactor{}
}

func (r *MyReactor) CreateInput(n int) InputCell {
	v := MyCell{}
	v.value = n
	v.callbacks = []*func(int){}
	v.dependants = []*MyCell{}
	return &v
}

func (r *MyReactor) CreateCompute1(source Cell, onChange func(int) int) ComputeCell {
	v := MyCell{}
	v.onChange = func() int { return onChange(source.Value()) }
	v.value = v.onChange()
	v.callbacks = []*func(int){}
	switch s := source.(type) {
	case *MyCell:
		s.dependants = append(s.dependants, &v)
	}
	return &v
}

func (r *MyReactor) CreateCompute2(source1 Cell, source2 Cell, onChange func(int, int) int) ComputeCell {
	v := MyCell{}
	v.onChange = func() int { return onChange(source1.Value(), source2.Value()) }
	v.value = v.onChange()
	v.callbacks = []*func(int){}
	switch s1 := source1.(type) {
	case *MyCell:
		switch s2 := source2.(type) {
		case *MyCell:
			s1.dependants = append(s1.dependants, &v)
			s2.dependants = append(s2.dependants, &v)
		}
	}
	return &v
}

type MyCell struct {
	value      int
	onChange   func() int
	callbacks  []*func(int)
	dependants []*MyCell
}

func (c *MyCell) Value() int {
	return c.value
}

func (c *MyCell) AddCallback(callback func(int)) Canceler {
	c.callbacks = append(c.callbacks, &callback)
	return &MyCanceler{c, &callback}
}

func (c *MyCell) CallCallbacks() {
	for _, cb := range c.callbacks {
		(*cb)(c.value)
	}
}

func (c *MyCell) SetValue(v int) {
	if c.value == v {
		return
	}

	c.value = v
	c.CallCallbacks()

	dirty := c.dependants
	next := []*MyCell{}
	for len(dirty) > 0 {
		for _, c := range dirty {
			dep := c.updateValue()
			next = append(next, dep...)
		}
		dirty = next
		next = []*MyCell{}
	}
}

func (c *MyCell) updateValue() []*MyCell {
	v := c.onChange()
	if c.value == v {
		return []*MyCell{}
	}
	c.value = v
	c.CallCallbacks()
	return c.dependants
}

type MyCanceler struct {
	cell     *MyCell
	callback *func(int)
}

func (c *MyCanceler) Cancel() {
	index := -1
	for i, cb := range c.cell.callbacks {
		if cb == c.callback {
			index = i
			break
		}
	}
	if index == -1 {
		return
	}
	c.cell.callbacks = append(c.cell.callbacks[:index], c.cell.callbacks[index+1:]...)
}
