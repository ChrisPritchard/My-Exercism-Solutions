package school

import "sort"

type Grade struct {
	Number   int
	Students []string
}

type School struct {
	grades []Grade
}

func New() *School {
	return &School{}
}

func (s *School) Add(student string, grade int) {
	for i := 0; i < len(s.grades); i++ {
		if s.grades[i].Number == grade {
			s.grades[i].Students = append(s.grades[i].Students, student)
			sort.Slice(s.grades[i].Students, func(a, b int) bool {
				return s.grades[i].Students[a] < s.grades[i].Students[b]
			})
			return
		}
	}
	s.grades = append(s.grades, Grade{grade, []string{student}})
	sort.Slice(s.grades, func(a, b int) bool { return s.grades[a].Number < s.grades[b].Number })
}

func (s *School) Grade(grade int) []string {
	for _, g := range s.grades {
		if g.Number == grade {
			return g.Students
		}
	}
	return []string{}
}

func (s *School) Enrollment() []Grade {
	return s.grades
}
