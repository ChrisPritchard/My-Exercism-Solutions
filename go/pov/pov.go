package pov

type Graph struct {
	arcs [][2]string
}

func New() *Graph {
	return &Graph{}
}

func (g *Graph) AddNode(nodeLabel string) {
}

func (g *Graph) AddArc(from, to string) {
	g.arcs = append(g.arcs, [2]string{from, to})
}

func (g *Graph) ArcList() []string {
	res := []string{}
	for _, a := range g.arcs {
		res = append(res, a[0]+" -> "+a[1])
	}
	return res
}

func (g *Graph) reverse(target, ignoreWithParent string) {
	for i, a := range g.arcs {
		if a[0] != ignoreWithParent && a[1] == target {
			g.arcs[i] = [2]string{a[1], a[0]}
			g.reverse(a[0], target)
		}
	}
}

func (g *Graph) ChangeRoot(oldRoot, newRoot string) *Graph {
	g.reverse(newRoot, "")
	return g
}
