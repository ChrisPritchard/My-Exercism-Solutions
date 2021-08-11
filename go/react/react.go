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
	return &v
}

func (r *MyReactor) CreateCompute1(source Cell, onChange func(int) int) ComputeCell {
	v := MyCell{}
	v.value = onChange(source.Value())
	v.callbacks = []*func(int){}
	switch s := source.(type) {
	case *MyCell:
		s.AddCallback(func(n int) { v.SetValue(onChange(source.Value())) })
	}
	return &v
}

func (r *MyReactor) CreateCompute2(source1 Cell, source2 Cell, onChange func(int, int) int) ComputeCell {
	v := MyCell{}
	v.value = onChange(source1.Value(), source2.Value())
	v.callbacks = []*func(int){}
	switch s1 := source1.(type) {
	case *MyCell:
		switch s2 := source2.(type) {
		case *MyCell:
			s1.AddCallback(func(n int) { v.SetValue(onChange(s1.Value(), s2.Value())) })
			s2.AddCallback(func(n int) { v.SetValue(onChange(s1.Value(), s2.Value())) })
		}
	}
	return &v
}

type MyCell struct {
	value     int
	callbacks []*func(int)
}

func (c *MyCell) Value() int {
	return c.value
}

func (c *MyCell) AddCallback(callback func(int)) Canceler {
	c.callbacks = append(c.callbacks, &callback)
	return &MyCanceler{c, &callback}
}

func (c *MyCell) SetValue(v int) {
	if c.value == v {
		return
	}
	c.value = v
	for _, c := range c.callbacks {
		(*c)(v)
	}
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
