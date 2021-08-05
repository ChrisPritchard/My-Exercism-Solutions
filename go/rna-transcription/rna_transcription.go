package strand

func ToRNA(dna string) string {
	result := ""
	for _, d := range dna {
		switch d {
		case 'G':
			result += "C"
		case 'C':
			result += "G"
		case 'T':
			result += "A"
		case 'A':
			result += "U"
		}
	}
	return result
}
