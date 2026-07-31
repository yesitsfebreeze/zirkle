// §head page/pkg/tui/render.go:14-23 Model.terminalContent
// §sig func (m Model) terminalContent() string
	var b strings.Builder
	if len(m.terminal) > 0 {
		for _, line := range m.terminal {
			b.WriteString(line)
			b.WriteByte('\n')
		}
	}
	return b.String()
// §foot page/pkg/tui/render.go Model.terminalContent