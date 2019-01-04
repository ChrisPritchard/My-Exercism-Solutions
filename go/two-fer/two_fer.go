// Package twofer contains a simple sharing function.
package twofer

import (
	"fmt"
)

// ShareWith prints a phrase either with a given name or 'you'.
func ShareWith(name string) string {
	if name == "" {
		name = "you"
	}
	return fmt.Sprintf("One for %s, one for me.", name)
}
