package circular

import (
	"errors"
)

type Buffer struct {
	buffer       []byte
	head, length int
}

func NewBuffer(size int) *Buffer {
	return &Buffer{buffer: make([]byte, size), head: 0, length: 0}
}

func (b *Buffer) ReadByte() (byte, error) {
	if b.length == 0 {
		return 0, errors.New("buffer is empty")
	}
	start := b.head - b.length
	if start < 0 {
		start += len(b.buffer)
	}
	b.length--
	return b.buffer[start], nil
}

func (b *Buffer) WriteByte(c byte) error {
	if b.length == len(b.buffer) {
		return errors.New("buffer is full")
	}
	b.buffer[b.head] = c
	b.length++
	b.head++
	if b.head >= len(b.buffer) {
		b.head = 0
	}
	return nil
}

func (b *Buffer) Overwrite(c byte) {
	if b.length != len(b.buffer) {
		b.WriteByte(c)
		return
	}
	start := b.head - b.length
	if start < 0 {
		start += len(b.buffer)
	}
	b.buffer[start] = c
	b.head++
}

func (b *Buffer) Reset() {
	b.length = 0
}
