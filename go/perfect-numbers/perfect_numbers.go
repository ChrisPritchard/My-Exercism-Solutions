package perfect

import "errors"

// Classification represents a type of perfect number
type Classification int

var (
	// ClassificationDeficient when aliquot sum < number
	ClassificationDeficient = Classification(0)
	// ClassificationPerfect when aliquot sum = number
	ClassificationPerfect = Classification(1)
	// ClassificationAbundant when aliquot sum > number
	ClassificationAbundant = Classification(2)
	// ErrOnlyPositive when given a number less than 1
	ErrOnlyPositive = errors.New("number must be above 0")
)

// Classify returns the classification for a given number
func Classify(n int64) (Classification, error) {
	if n < 1 {
		return 0, ErrOnlyPositive
	}

	aliquot := int64(0)
	for i := int64(1); i < n; i++ {
		if n%i == 0 {
			aliquot += i
		}
	}

	if aliquot < n {
		return ClassificationDeficient, nil
	} else if aliquot == n {
		return ClassificationPerfect, nil
	} else {
		return ClassificationAbundant, nil
	}
}
