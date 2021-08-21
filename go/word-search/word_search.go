package wordsearch

import (
	"fmt"
)

func crawl(wordRem string, x, y, dx, dy int, puzzle []string) (*[2]int, bool) {
	if y < 0 || y >= len(puzzle) || x < 0 || x >= len(puzzle[y]) {
		return nil, false
	}
	if puzzle[y][x] != wordRem[0] {
		return nil, false
	}
	if len(wordRem) == 1 {
		return &[2]int{x, y}, true
	}
	return crawl(wordRem[1:], x+dx, y+dy, dx, dy, puzzle)
}

func crawler(word string, x, y int, puzzle []string) (*[2][2]int, bool) {
	if puzzle[y][x] != word[0] {
		return nil, false
	}
	for dx := -1; dx <= 1; dx++ {
		for dy := -1; dy <= 1; dy++ {
			if dx == dy && dx == 0 {
				continue
			}
			res, success := crawl(word[1:], x+dx, y+dy, dx, dy, puzzle)
			if success {
				return &[2][2]int{
					{x, y},
					(*res),
				}, true
			}
		}
	}
	return nil, false
}

func Solve(words []string, puzzle []string) (map[string][2][2]int, error) {
	result := make(map[string][2][2]int)
	for y := 0; y < len(puzzle); y++ {
		for x := 0; x < len(puzzle[y]); x++ {
			for _, w := range words {
				if _, exists := result[w]; exists {
					continue
				}
				res, success := crawler(w, x, y, puzzle)
				if success {
					result[w] = *res
				}
			}
		}
	}
	for _, w := range words {
		if _, exists := result[w]; !exists {
			return result, fmt.Errorf("word '%s' was not found", w)
		}
	}
	return result, nil
}
