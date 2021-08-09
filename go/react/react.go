package react

type MyReactor struct {
}

func New() *MyReactor {
	return &MyReactor{}
}

func (r *MyReactor) CreateInput(n int) InputCell {
	return &MyInputCell{}
}

func (r *MyReactor) CreateCompute1(source Cell, onChange func(int) int) ComputeCell {
	return &MyComputeCell{}
}

func (r *MyReactor) CreateCompute2(source1 Cell, source2 Cell, onChange func(int, int) int) ComputeCell {
	return &MyComputeCell{}
}

type MyCell struct {
	value int
}

func (c *MyCell) Value() int {
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
