package protein

import "errors"

// STOP indicates a stop protein has been encountered
var STOP = errors.New("Processing terminated")

// ErrInvalidBase indicates a base has been found that is not recognised
var ErrInvalidBase = errors.New("Unrecognised base")

// FromCodon returns the appropriate protein from a given codon
func FromCodon(input string) (protein string, error error) {
	switch input {
	case "AUG":
		protein = "Methionine"
	case "UUU", "UUC":
		protein = "Phenylalanine"
	case "UUA", "UUG":
		protein = "Leucine"
	case "UCU", "UCC", "UCA", "UCG":
		protein = "Serine"
	case "UAU", "UAC":
		protein = "Tyrosine"
	case "UGU", "UGC":
		protein = "Cysteine"
	case "UGG":
		protein = "Tryptophan"
	case "UAA", "UAG", "UGA":
		error = STOP
	default:
		error = ErrInvalidBase
	}
	return
}

// FromRNA processes an RNA string and returns a set of proteins
func FromRNA(input string) (result []string, error error) {
	for i := 0; i < len(input); i += 3 {
		codon := input[i : i+3]
		protein, codonError := FromCodon(codon)
		if codonError == STOP {
			return
		} else if codonError != nil {
			error = codonError
			return
		}
		result = append(result, protein)
	}
	return
}
