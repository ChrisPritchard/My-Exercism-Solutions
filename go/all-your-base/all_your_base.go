package allyourbase

import (
	"fmt"
	"math"
)

func ConvertToBase(inputBase int, inputDigits []int, outputBase int) ([]int, error) {
	result := []int{}
	if inputBase < 2 {
		return result, fmt.Errorf("input base must be >= 2")
	}
	if outputBase < 2 {
		return result, fmt.Errorf("output base must be >= 2")
	}
	base10 := 0
	for i, v := range inputDigits {
		if v >= inputBase || v < 0 {
			return result, fmt.Errorf("all digits must satisfy 0 <= d < input base")
		}
		base10 += v * int(math.Pow(float64(inputBase), float64(len(inputDigits)-(i+1))))
	}

	return convert(base10, outputBase, result), nil
}

func convert(remainder, outputBase int, result []int) []int {
	digit := remainder % outputBase
	result = append([]int{digit}, result...)
	quotient := int(math.Floor(float64(remainder) / float64(outputBase)))
	if quotient == 0 {
		return result
	}
	return convert(quotient, outputBase, result)
}
