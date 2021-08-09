package react

// idea, maintain a set of dependents
// when ever a input cell or compute cell value changes
// find all dependents, and update them
// dependents can only be compute cells by definition
//

type MyReactor struct {
	directs []direct
	doubles []double
}

type direct struct {
	target   Cell
	source   Cell
	onChange func(int) int
}

type double struct {
	target   Cell
	source1  Cell
	source2  Cell
	onChange func(int, int) int
}

func New() *MyReactor {
	return &MyReactor{[]direct{}, []double{}}
}

func (r MyReactor) updateFor(c MyCell) {

}

func (r *MyReactor) CreateInput(n int) InputCell {
	v := MyInputCell{}
	v.value = n
	v.reactor = r
	return &v
}

func (r *MyReactor) CreateCompute1(source Cell, onChange func(int) int) ComputeCell {
	v := MyComputeCell{}
	v.reactor = r
	v.value = onChange(source.Value())
	r.directs = append(r.directs, direct{v, source, onChange})
	return &v
}

func (r *MyReactor) CreateCompute2(source1 Cell, source2 Cell, onChange func(int, int) int) ComputeCell {
	v := MyComputeCell{}
	v.reactor = r
	v.value = onChange(source1.Value(), source2.Value())
	r.doubles = append(r.doubles, double{v, source1, source2, onChange})
	return &v
}

type MyCell struct {
	value   int
	reactor *MyReactor
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
	c.reactor.updateFor(c)
}

type MyComputeCell struct {
	MyCell
}

func (c *MyComputeCell) AddCallback(callback func(int)) Canceler {
	return &MyCanceler{}
}

type MyCanceler struct {
}

func (c *MyCanceler) Cancel() {

}
