package paasio

import (
	"io"
	"sync"
)

type readCounter struct {
	baseReader io.Reader
	bytes      int64
	reads      int
	sync.Mutex
}

type writeCounter struct {
	baseWriter io.Writer
	bytes      int64
	writes     int
	sync.Mutex
}

type readWriteCounter struct {
	readCounter
	writeCounter
}

// Read implements io.Read
func (reader *readCounter) Read(p []byte) (n int, err error) {
	n, err = reader.baseReader.Read(p)
	reader.Lock()
	defer reader.Unlock()
	reader.bytes += int64(n)
	reader.reads++
	return
}

// ReadCount returns the total number of bytes successfully read along
// with the total number of calls to Read().
func (reader *readCounter) ReadCount() (n int64, nops int) {
	return reader.bytes, reader.reads
}

// Write implements io.Write
func (writer *writeCounter) Write(p []byte) (n int, err error) {
	n, err = writer.baseWriter.Write(p)
	writer.Lock()
	defer writer.Unlock()
	writer.bytes += int64(n)
	writer.writes++
	return
}

// WriteCount returns the total number of bytes successfully written along
// with the total number of calls to Write().
func (writer *writeCounter) WriteCount() (n int64, nops int) {
	return writer.bytes, writer.writes
}

// NewReadCounter returns a new read counting implementation
func NewReadCounter(base io.Reader) ReadCounter {
	return &readCounter{base, 0, 0, sync.Mutex{}}
}

// NewWriteCounter returns a new write counting implementation
func NewWriteCounter(base io.Writer) WriteCounter {
	return &writeCounter{base, 0, 0, sync.Mutex{}}
}

// NewReadWriteCounter returns a combination read/write counter
func NewReadWriteCounter(base io.ReadWriter) ReadWriteCounter {
	return &readWriteCounter{
		readCounter{base, 0, 0, sync.Mutex{}},
		writeCounter{base, 0, 0, sync.Mutex{}},
	}
}
