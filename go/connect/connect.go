package connect

import "errors"

var adjacent = [][2]int{{0, -1}, {1, -1}, {1, 0}, {0, 1}, {-1, 1}, {-1, 0}}

func crawl(board []string, targets, visited map[[2]int]bool, edges [][2]int, valid byte) bool {
	for len(edges) != 0 {
		newEdges := [][2]int{}
		for _, k := range edges {
			if _, exists := targets[k]; exists {
				return true
			}
			for _, d := range adjacent {
				x := k[0] + d[0]
				y := k[1] + d[1]
				if y < 0 || y >= len(board) || x < 0 || x >= len(board[0]) || board[y][x] != valid {
					continue
				}
				p := [2]int{x, y}
				if _, exists := visited[p]; !exists {
					newEdges = append(newEdges, p)
					visited[p] = true
				}
			}
		}
		edges = newEdges
	}
	return false
}

func ResultOf(board []string) (string, error) {
	if len(board) == 0 {
		return "", errors.New("invalid board")
	}

	xTargets := make(map[[2]int]bool)
	xStart := make(map[[2]int]bool)
	xEdges := [][2]int{}
	for y := 0; y < len(board); y++ {
		if board[y][0] == 'X' {
			xStart[[2]int{0, y}] = true
			xEdges = append(xEdges, [2]int{0, y})
		}
		if board[y][len(board[0])-1] == 'X' {
			xTargets[[2]int{len(board[0]) - 1, y}] = true
		}
	}
	if len(xStart) > 0 && len(xTargets) > 0 && crawl(board, xTargets, xStart, xEdges, 'X') {
		return "X", nil
	}

	oTargets := make(map[[2]int]bool)
	oStart := make(map[[2]int]bool)
	oEdges := [][2]int{}
	for x := 0; x < len(board[0]); x++ {
		if board[0][x] == 'O' {
			oStart[[2]int{x, 0}] = true
			oEdges = append(oEdges, [2]int{x, 0})
		}
		if board[len(board)-1][x] == 'O' {
			oTargets[[2]int{x, len(board) - 1}] = true
		}
	}
	if len(oStart) > 0 && len(oTargets) > 0 && crawl(board, oTargets, oStart, oEdges, 'O') {
		return "O", nil
	}

	return "", nil
}
