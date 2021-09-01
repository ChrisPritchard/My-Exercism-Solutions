package ocr

import (
	"reflect"
	"strings"
)

var digits = map[string]string{
	`
 _ 
| |
|_|
   `: "0",
	`
   
  |
  |
   `: "1",
	`
 _ 
 _|
|_ 
   `: "2",
	`
 _ 
 _|
 _|
   `: "3",
	`
   
|_|
  |
   `: "4",
	`
 _ 
|_ 
 _|
   `: "5",
	`
 _ 
|_ 
|_|
   `: "6",
	`
 _ 
  |
  |
   `: "7",
	`
 _ 
|_|
|_|
   `: "8",
	`
 _ 
|_|
 _|
   `: "9",
}

func recognizeDigit(in string) string {
	for k, v := range digits {
		if reflect.DeepEqual(in, strings.Trim(k, "\n")) {
			return v
		}
	}
	return "?"
}

func Recognize(in string) []string {
	lines := strings.Split(strings.Trim(in, "\n"), "\n")
	result := []string{}
	for i := 0; i < len(lines); i += 4 {
		parsed := ""
		for j := 0; j < len(lines[i]); j += 3 {
			cell := lines[i][j:j+3] + "\n" +
				lines[i+1][j:j+3] + "\n" +
				lines[i+2][j:j+3] + "\n" +
				lines[i+3][j:j+3]
			parsed += recognizeDigit(cell)
		}
		result = append(result, parsed)
	}
	return result
}
