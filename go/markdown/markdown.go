package markdown

// implementation to refactor

// Er, this is actually pretty good.
// Only slight improvement to support more than one instance of bold/italic

import (
	"fmt"
)

// Render translates markdown to HTML
func Render(markdown string) string {
	emphasis := 0
	header := 0
	pos := 0
	list := 0
	html := ""
	for {
		if pos >= len(markdown) {
			break
		}
		char := markdown[pos]
		if char == '_' {
			if emphasis == 1 {
				html += "</em>"
				emphasis = 0
				pos++
				continue
			} else if emphasis == 2 {
				html += "</strong>"
				emphasis = 0
				pos += 2
				continue
			}
			for char == '_' {
				emphasis++
				pos++
				char = markdown[pos]
			}
			if emphasis == 1 {
				html += "<em>"
			} else if emphasis == 2 {
				html += "<strong>"
			}
			continue
		}
		if char == '#' {
			for char == '#' {
				header++
				pos++
				char = markdown[pos]
			}
			html += fmt.Sprintf("<h%d>", header)
			pos++
			continue
		}
		if char == '*' {
			if list == 0 {
				html += "<ul>"
			}
			html += "<li>"
			list++
			pos += 2
			continue
		}
		if char == '\n' {
			if list > 0 {
				html += "</li>"
			}
			if header > 0 {
				html += fmt.Sprintf("</h%d>", header)
				header = 0
			}
			pos++
			continue
		}
		html += string(char)
		pos++
	}
	if header > 0 {
		return html + fmt.Sprintf("</h%d>", header)
	}
	if list > 0 {
		return html + "</li></ul>"
	}
	if emphasis == 1 {
		html += "</em>"
	}
	if emphasis == 2 {
		html += "</strong>"
	}
	return "<p>" + html + "</p>"

}
