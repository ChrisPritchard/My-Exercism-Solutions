package say

import "strings"

var base = []string{
	"zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine",
}
var teens = []string{
	"ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen",
}
var decades = []string{
	"twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety",
}

func say(number int64) string {
	if number < 10 {
		return base[number]
	} else if number < 20 {
		return teens[number-10]
	} else if number < 100 && number%10 == 0 {
		return decades[number-20]
	} else if number < 100 {
		return decades[(number-20)/10] + "-" + base[number%10]
	} else if number < 1000 {
		return say(number/100) + " hundred " + say(number%100)
	} else if number < 1000000 {
		return say(number/1000) + " thousand " + say(number%1000)
	} else if number < 1000000000 {
		return say(number/1000000) + " million " + say(number%1000000)
	} else {
		return say(number/1000000000) + " billion " + say(number%1000000000)
	}
}

func Say(number int64) (string, bool) {
	if number < 0 || number > 999999999999 {
		return "", false
	}
	return strings.ReplaceAll(say(number), " zero", ""), true
}
