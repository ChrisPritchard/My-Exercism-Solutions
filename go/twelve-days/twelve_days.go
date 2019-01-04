package twelve

import (
	"fmt"
	"strings"
)

var days = []string{
	"first", "second", "third", "fourth",
	"fifth", "sixth", "seventh", "eighth",
	"ninth", "tenth", "eleventh", "twelfth"}

var gifts = []string{
	"a Partridge in a Pear Tree.",
	"two Turtle Doves, ", "three French Hens, ",
	"four Calling Birds, ", "five Gold Rings, ",
	"six Geese-a-Laying, ", "seven Swans-a-Swimming, ",
	"eight Maids-a-Milking, ", "nine Ladies Dancing, ",
	"ten Lords-a-Leaping, ", "eleven Pipers Piping, ",
	"twelve Drummers Drumming, ",
}

// Verse returns a verse of the twelve days at christmas
func Verse(n int) string {
	var result strings.Builder
	result.WriteString(fmt.Sprintf("On the %s day of Christmas my true love gave to me: ", days[n-1]))
	for v := n; v >= 1; v-- {
		if v == 1 && n != 1 {
			result.WriteString("and ")
		}
		result.WriteString(gifts[v-1])
	}
	return result.String()
}

// Song returns all verses of the twelve days at christmas
func Song() string {
	var result strings.Builder
	for i := 1; i <= 12; i++ {
		result.WriteString(Verse(i) + "\n")
	}
	return result.String()
}
