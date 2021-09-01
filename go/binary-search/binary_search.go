package binarysearch

func SearchInts(sorted []int, target int) int {
	if len(sorted) == 0 {
		return -1
	}
	mid := len(sorted) / 2
	if target == sorted[mid] {
		return mid
	}
	if target < sorted[mid] {
		return SearchInts(sorted[:mid], target)
	}
	higher := SearchInts(sorted[mid+1:], target)
	if higher == -1 {
		return -1
	}
	return mid + 1 + higher
}
