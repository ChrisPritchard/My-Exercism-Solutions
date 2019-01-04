package house

import (
	"fmt"
	"strings"
)

var pairs = [][2]string{
	[2]string{"house that Jack built", ""},
	[2]string{"malt", "lay in"},
	[2]string{"rat", "ate"},
	[2]string{"cat", "killed"},
	[2]string{"dog", "worried"},
	[2]string{"cow with the crumpled horn", "tossed"},
	[2]string{"maiden all forlorn", "milked"},
	[2]string{"man all tattered and torn", "kissed"},
	[2]string{"priest all shaven and shorn", "married"},
	[2]string{"rooster that crowed in the morn", "woke"},
	[2]string{"farmer sowing his corn", "kept"},
	[2]string{"horse and the hound and the horn", "belonged to"},
}

// Song returns all verses of the song
func Song() string {
	var composer strings.Builder
	for i := 0; i < len(pairs); i++ {
		composer.WriteString(Verse(i + 1))
		if i != len(pairs)-1 {
			composer.WriteString("\n\n")
		}
	}
	return composer.String()
}

// Verse returns a single verse of the song
func Verse(n int) string {
	var composer strings.Builder
	for i := n; i >= 1; i-- {
		first := "This is the"
		if i != n {
			first = fmt.Sprintf("that %s the", pairs[i][1])
		}

		line := fmt.Sprintf("%s %s", first, pairs[i-1][0])
		composer.WriteString(line)

		if i == 1 {
			composer.WriteRune('.')
		} else {
			composer.WriteRune('\n')
		}
	}
	return composer.String()
}
