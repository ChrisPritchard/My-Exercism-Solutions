package diffsquares

// SquareOfSum returns the square of the sum of the first N natural numbers
func SquareOfSum(n int) int {
	sum := n * (n + 1) / 2
	return sum * sum
}

// SumOfSquares returns the sum of the first N natural numbers squared
func SumOfSquares(n int) int {
	return n * (n + 1) * (2*n + 1) / 6
}

// Difference returns the difference between the square of sums and sum of squares
func Difference(n int) int {
	return SquareOfSum(n) - SumOfSquares(n)
}
