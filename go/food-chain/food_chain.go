package foodchain

import (
	"fmt"
	"strings"
)

var elements = [][3]string{
	[3]string{"fly", "I don't know why she swallowed the fly. Perhaps she'll die.", "."},
	[3]string{"spider", "It wriggled and jiggled and tickled inside her.", " that wriggled and jiggled and tickled inside her."},
	[3]string{"bird", "How absurd to swallow a bird!", "."},
	[3]string{"cat", "Imagine that, to swallow a cat!", "."},
	[3]string{"dog", "What a hog, to swallow a dog!", "."},
	[3]string{"goat", "Just opened her throat and swallowed a goat!", "."},
	[3]string{"cow", "I don't know how she swallowed a cow!", "."},
	[3]string{"horse", "She's dead, of course!", "."},
}

// Verse returns a given verse of the song
func Verse(n int) string {
	var composer strings.Builder

	composer.WriteString(fmt.Sprintf("I know an old lady who swallowed a %s.\n", elements[n-1][0]))
	composer.WriteString(elements[n-1][1])

	if n == len(elements) || n == 1 {
		return composer.String()
	}

	composer.WriteRune('\n')
	for i := n; i > 1; i-- {
		line := fmt.Sprintf("She swallowed the %s to catch the %s%s\n", elements[i-1][0], elements[i-2][0], elements[i-2][2])
		composer.WriteString(line)
	}

	if n != 1 {
		composer.WriteString(elements[0][1])
	}

	return composer.String()
}

// Verses returns a range of verses from the song
func Verses(start, end int) string {
	var composer strings.Builder
	for i := start; i <= end; i++ {
		composer.WriteString(Verse(i))
		if i != end {
			composer.WriteString("\n\n")
		}
	}
	return composer.String()
}

// Song returns the entire song
func Song() string {
	return Verses(1, len(elements))
}
