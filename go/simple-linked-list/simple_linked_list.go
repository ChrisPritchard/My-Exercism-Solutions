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

func rev(s []int) []int {
	rev := make([]int, len(s))
	for i := 0; i < len(s); i++ {
		rev[len(rev)-1-i] = s[i]
	}
	return rev
}

func New(data []int) *List {
	data = rev(data)
	if len(data) == 0 {
		return &List{nil, 0}
	}
	head := &Element{data[0], nil}
	current := head
	for i := 1; i < len(data); i++ {
		current.next = &Element{data[i], nil}
		current = current.next
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
		res = append(res, current.data)
		current = current.next
	}
	return rev(res)
}

func (l *List) Reverse() *List {
	return New(rev(l.Array()))
}
