package tree

import (
	"errors"
	"fmt"
	"sort"
)

// Record an original database representation of nodes
type Record struct {
	ID, Parent int
}

// Node the proper in memory representation of nodes
type Node struct {
	ID       int
	Children []*Node
}

// Build converts records into nodes
func Build(records []Record) (*Node, error) {
	if len(records) == 0 {
		return nil, nil
	}

	dupe := make(map[int]bool)
	childrenMap := make(map[int][]int)

	for _, r := range records {
		if r.ID >= len(records) {
			return nil, fmt.Errorf("non-continuous")
		}

		if dupe[r.ID] {
			return nil, fmt.Errorf("duplicate node")
		}

		if r.ID < r.Parent {
			return nil, errors.New("higher id parent of lower id")
		}

		dupe[r.ID] = true
		if r.ID == r.Parent {
			continue
		}

		childrenMap[r.Parent] = append(childrenMap[r.Parent], r.ID)
		sort.Ints(childrenMap[r.Parent])
	}

	root := &Node{}
	todo := []*Node{root}

	n := 1
	for {
		if len(todo) == 0 {
			break
		}

		newTodo := []*Node(nil)

		for _, c := range todo {
			for _, rid := range childrenMap[c.ID] {
				n++
				nn := &Node{ID: rid}
				c.Children = append(c.Children, nn)
				newTodo = append(newTodo, nn)
			}
		}

		todo = newTodo
	}

	if n != len(records) {
		return nil, fmt.Errorf("non-continuous")
	}

	return root, nil
}
