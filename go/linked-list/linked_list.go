package linkedlist

type Node struct {
	Val        interface{}
	prev, next *Node
}

type List struct {
	head, tail *Node
}

var ErrEmptyList error

func (e *Node) Next() *Node {
	if e == nil {
		return nil
	}
	return e.next
}

func (e *Node) Prev() *Node {
	if e == nil {
		return nil
	}
	return e.prev
}

func NewList(args ...interface{}) *List {
	var head *Node = nil
	tail := head
	for _, v := range args {
		if head == nil {
			head = &Node{v, nil, nil}
			tail = head
		} else {
			newTail := &Node{v, tail, nil}
			tail.next = newTail
			tail = newTail
		}
	}
	return &List{head, tail}
}

func (l *List) PushFront(v interface{}) {
	newHead := &Node{v, nil, l.head}
	if l.head != nil {
		l.head.prev = newHead
	}
	l.head = newHead
	if l.tail == nil {
		l.tail = l.head
	}
}

func (l *List) PushBack(v interface{}) {
	newTail := &Node{v, l.tail, nil}
	if l.tail != nil {
		l.tail.next = newTail
	}
	l.tail = newTail
	if l.head == nil {
		l.head = l.tail
	}
}

func (l *List) PopFront() (interface{}, error) {
	if l.head == nil {
		return nil, ErrEmptyList
	}
	v := l.head.Val
	l.head = l.head.next
	if l.head != nil {
		l.head.prev = nil
	}
	if l.head == nil {
		l.tail = nil
	}
	return v, nil
}

func (l *List) PopBack() (interface{}, error) {
	if l.tail == nil {
		return nil, ErrEmptyList
	}
	v := l.tail.Val
	l.tail = l.tail.prev
	if l.tail != nil {
		l.tail.next = nil
	}
	if l.tail == nil {
		l.head = nil
	}
	return v, nil
}

func (l *List) Reverse() *List {
	current, op := l.head, l.head
	for current != nil {
		op = current
		current = current.next
		op.next = op.prev
		op.prev = current
	}
	l.tail = l.head
	l.head = op
	return l
}

func (l *List) First() *Node {
	return l.head
}

func (l *List) Last() *Node {
	return l.tail
}
