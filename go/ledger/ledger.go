package ledger

import (
	"errors"
	"fmt"
	"sort"
	"time"
)

type Entry struct {
	Date        string // "Y-m-d"
	Description string
	Change      int // in cents
}

// My refactor process: assign original developer to documentation until he quits, then rewrite all his shitty code from scratch

type localeConfig struct {
	header, dateFormat       string
	subDollar, aboveThousand string
	spaceCurrency            bool
	negative                 func(string) string
}

var locales = map[string]localeConfig{
	"en-US": {"Date       | Description               | Change", "01/02/2006", ".", ",", false, func(s string) string { return fmt.Sprintf("(%s%s)", s[0:1], s[1:len(s)-1]) }},
	"nl-NL": {"Datum      | Omschrijving              | Verandering", "02-01-2006", ",", ".", true, func(s string) string { return s[:len(s)-1] + "-" }},
}

var currencies = map[string]string{
	"USD": "$",
	"EUR": "€",
}

func FormatLedger(currency string, locale string, entries []Entry) (string, error) {
	if _, exists := currencies[currency]; !exists {
		return "", errors.New("invalid currency: " + currency)
	}
	if _, exists := locales[locale]; !exists {
		return "", errors.New("invalid locale: " + locale)
	}

	result := locales[locale].header + "\n"

	sorted := append([]Entry{}, entries...)
	sort.Slice(sorted, func(i, j int) bool {
		if sorted[i].Date != sorted[j].Date {
			return sorted[i].Date < sorted[j].Date
		}
		return sorted[i].Change < sorted[j].Change
	})

	for _, entry := range sorted {
		date, err := time.Parse("2006-01-02", entry.Date)
		if err != nil {
			return "", errors.New("invalid date: " + entry.Date)
		}
		dateFormatted := date.Format(locales[locale].dateFormat)

		description := entry.Description
		if len(description) > 25 {
			description = description[:22] + "..."
		}

		change := formatChange(entry.Change, locale, currency)

		result += fmt.Sprintf("%s | %-25s | %13s\n", dateFormatted, description, change)
	}

	return result, nil
}

func formatChange(change int, locale, currency string) string {
	rawChange := change
	if rawChange < 0 {
		rawChange *= -1
	}
	sub := rawChange % 100
	above := rawChange / 100
	result := fmt.Sprintf("%d%s%02d", above%1000, locales[locale].subDollar, sub)
	if above >= 1000 {
		result = fmt.Sprintf("%d%s%s", above/1000, locales[locale].aboveThousand, result)
	}
	if locales[locale].spaceCurrency {
		result = " " + result
	}
	result = currencies[currency] + result + " "
	if change < 0 {
		result = locales[locale].negative(result)
	}
	return result
}
