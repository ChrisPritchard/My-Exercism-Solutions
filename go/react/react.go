package react

type MyReactor struct {
}

func New() *MyReactor {
	return &MyReactor{}
}

func (r *MyReactor) CreateInput(n int) InputCell {
	v := MyCell{}
	v.getValue = func() int { return v.value }
	v.value = n
	return &v
}

func (r *MyReactor) CreateCompute1(source Cell, onChange func(int) int) ComputeCell {
	v := MyCell{}
	v.getValue = func() int { return onChange(source.Value()) }
	v.value = v.getValue()
	source.(*MyCell).addDependency(&v)
	return &v
}

func (r *MyReactor) CreateCompute2(source1 Cell, source2 Cell, onChange func(int, int) int) ComputeCell {
	v := MyCell{}
	v.getValue = func() int { return onChange(source1.Value(), source2.Value()) }
	v.value = v.getValue()
	source1.(*MyCell).addDependency(&v)
	source2.(*MyCell).addDependency(&v)
	return &v
}

type MyCell struct {
	value      int
	dependents []*MyCell
	getValue   func() int
	callbacks  []*func(int)
}

func (c *MyCell) addDependency(dep *MyCell) {
	c.dependents = append(c.dependents, dep)
}

func (c *MyCell) Value() int {
	return c.getValue()
}

func (c *MyCell) SetValue(v int) {
	if c.value != v {
		c.value = v
		callCallbacks(c.dependents)
	}
}

func callCallbacks(deps []*MyCell) {
	for _, c := range deps {
		nv := c.Value()
		if c.value != nv {
			c.value = nv
			for _, cb := range c.callbacks {
				(*cb)(nv)
			}
			callCallbacks(c.dependents)
		}
	}
}

func (c *MyCell) AddCallback(callback func(int)) Canceler {
	c.callbacks = append(c.callbacks, &callback)
	return &MyCanceler{c, &callback}
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
