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
	v := MyInputCell{}
	v.value = n
	v.onChange = func(int) {}
	return &v
}

func (r *MyReactor) CreateCompute1(source Cell, onChange func(int) int) ComputeCell {
	v := MyComputeCell{}
	v.value = onChange(source.Value())
	v.onChange = func(int) {}
	v.callbacks = []func(int){}
	switch s := source.(type) {
	case *MyInputCell:
		s.onChange = func(n int) {
			s.onChange(n)
			nd := onChange(n)
			if nd != v.value {
				v.value = nd
				v.onChange(nd)
			}
		}
	case *MyComputeCell:
		s.onChange = func(n int) {
			s.onChange(n)
			nd := onChange(n)
			if nd != v.value {
				v.value = nd
				v.onChange(nd)
			}
		}
	}
	return &v
}

func (r *MyReactor) CreateCompute2(source1 Cell, source2 Cell, onChange func(int, int) int) ComputeCell {
	v := MyComputeCell{}
	v.value = onChange(source1.Value(), source2.Value())
	v.onChange = func(int) {}
	v.callbacks = []func(int){}
	return &v
}

type MyCell struct {
	value    int
	onChange func(int)
}

func (c MyCell) Value() int {
	return c.value
}

type MyInputCell struct {
	MyCell
}

func (c *MyInputCell) SetValue(v int) {
	if c.value == v {
		return
	}
	c.value = v
	c.onChange(v)
}

type MyComputeCell struct {
	MyCell
	callbacks []func(int)
}

func (c *MyComputeCell) AddCallback(callback func(int)) Canceler {
	index := len(c.callbacks)
	c.callbacks = append(c.callbacks, callback)
	return &MyCanceler{c, index}
}

type MyCanceler struct {
	computeCell *MyComputeCell
	index       int
}

func (c *MyCanceler) Cancel() {
	c.computeCell.callbacks = append(c.computeCell.callbacks[:c.index], c.computeCell.callbacks[c.index+1:]...)
}
