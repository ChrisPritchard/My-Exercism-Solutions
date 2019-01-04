package tournament

import (
	"bufio"
	"errors"
	"fmt"
	"io"
	"sort"
	"strings"
)

type team struct {
	wins, losses, draws int
}

func (team team) Score() int {
	return team.wins*3 + team.draws
}

// Tally calculates the scores for all teams from a match list input
func Tally(allMatches io.Reader, allResults io.Writer) error {
	matchReader := bufio.NewScanner(allMatches)
	teams := map[string]*team{}

	for matchReader.Scan() {
		line := matchReader.Text()
		if line == "" || line[0] == '#' {
			continue
		}

		aspects := strings.Split(line, ";")
		if len(aspects) != 3 {
			return errors.New("invalid line")
		}

		teamA, teamB, result := aspects[0], aspects[1], aspects[2]
		if _, ok := teams[teamA]; !ok {
			teams[teamA] = &team{}
		}
		if _, ok := teams[teamB]; !ok {
			teams[teamB] = &team{}
		}

		if result == "win" {
			teams[teamA].wins++
			teams[teamB].losses++
		} else if result == "draw" {
			teams[teamA].draws++
			teams[teamB].draws++
		} else if result == "loss" {
			teams[teamA].losses++
			teams[teamB].wins++
		} else {
			return errors.New("invalid line")
		}
	}

	_, err := fmt.Fprintf(allResults, "%-31s| MP |  W |  D |  L |  P\n", "Team")
	if err != nil {
		return err
	}

	for _, key := range sortedKeys(teams) {
		team := teams[key]
		mp := team.wins + team.losses + team.draws
		_, err := fmt.Fprintf(allResults, "%-31s|  %d |  %d |  %d |  %d |  %d\n",
			key, mp, team.wins, team.draws, team.losses, team.Score())
		if err != nil {
			return err
		}
	}

	return nil
}

func sortedKeys(teams map[string]*team) []string {
	var keys []string
	for key := range teams {
		keys = append(keys, key)
	}

	sort.Slice(keys, func(a, b int) bool {
		scoreA := teams[keys[a]].Score()
		scoreB := teams[keys[b]].Score()
		if scoreA == scoreB {
			return keys[a] < keys[b]
		}
		return scoreA > scoreB
	})

	return keys
}
