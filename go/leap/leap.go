package leap

// IsLeapYear returns whether a given year is a leap year
func IsLeapYear(year int) bool {
	return year%4 == 0 && year%100 != 0 || year%400 == 0
}
