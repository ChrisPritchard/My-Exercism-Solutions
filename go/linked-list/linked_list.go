package linkedlist

import "errors"

type Node struct {
	Val        interface{}
	next, prev *Node
}

type List struct {
	head, tail *Node
}

var ErrEmptyList error

func (e *Node) Next() *Node {
	if e == nil || e.next == e {
		return nil
	}
	return e.next
}

func (e *Node) Prev() *Node {
	if e == nil || e.prev == e {
		return nil
	}
	return e.prev
}

func NewList(args ...interface{}) *List {
	var tail *Node = nil
	head := tail
	for _, v := range args {
		if tail == nil {
			tail = &Node{v, nil, nil}
			head = tail
		} else {
			newHead := &Node{v, head, nil}
			head.next = newHead
			head = newHead
		}
	}
	return &List{head, tail}
}

func (l *List) PushFront(v interface{}) {
	newHead := &Node{v, l.head, nil}
	if l.head != nil {
		l.head.next = newHead
	}
	l.head = newHead
}

func (l *List) PushBack(v interface{}) {
	newTail := &Node{v, nil, l.tail}
	if l.tail != nil {
		l.tail.prev = newTail
	}
	l.tail = newTail
}

func (l *List) PopFront() (interface{}, error) {
	if l.head == nil {
		return nil, errors.New("list is empty")
	}
	v := l.head.Val
	l.head = l.head.prev
	if l.head != nil {
		l.head.next = nil
	}
	return v, nil
}

func (l *List) PopBack() (interface{}, error) {
	if l.tail == nil {
		return nil, errors.New("list is empty")
	}
	v := l.tail.Val
	l.tail = l.tail.next
	if l.tail != nil {
		l.tail.prev = nil
	}
	return v, nil
}

func (l *List) Reverse() *List {
	r := NewList()
	current := l.head
	for current != nil {
		r.PushFront(current.Val)
		current = current.prev
	}
	return r
}

func (l *List) First() *Node {
	return l.head
}

func (l *List) Last() *Node {
	return l.tail
}
