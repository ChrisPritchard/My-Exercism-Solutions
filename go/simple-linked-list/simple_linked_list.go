package linkedlist

import (
	"errors"
)

type Element struct {
	data int
	next *Element
}

type List struct {
	head *Element
	size int
}

func New(data []int) *List {
	var head *Element = nil
	for i := 0; i < len(data); i++ {
		head = &Element{data[i], head}
	}
	return &List{head, len(data)}
}

func (l *List) Size() int {
	return l.size
}

func (l *List) Push(d int) {
	newHead := &Element{d, l.head}
	l.head = newHead
	l.size++
}

func (l *List) Pop() (int, error) {
	if l.size == 0 {
		return 0, errors.New("list is empty")
	}
	toReturn := l.head.data
	l.head = l.head.next
	l.size--
	return toReturn, nil
}

func (l *List) Array() []int {
	res := []int{}
	current := l.head
	for current != nil {
		res = append([]int{current.data}, res...)
		current = current.next
	}
	return res
}

func (l *List) Reverse() *List {
	n := &List{nil, 0}
	current := l.head
	for current != nil {
		n.Push(current.data)
		current = current.next
	}
	return n
}
