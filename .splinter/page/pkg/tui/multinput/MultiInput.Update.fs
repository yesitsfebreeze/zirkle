// §head page/pkg/tui/multinput.go:49-65 MultiInput.Update
// §sig func (mi MultiInput) Update(msg tea.Msg) (MultiInput, tea.Cmd, BoundaryEvent)
	if k, ok := msg.(tea.KeyMsg); ok {
		switch k.String() {
		case "up":
			if mi.ta.Line() == 0 {
				return mi, nil, BoundaryTop
			}
		case "down":
			if mi.ta.Line() >= mi.ta.LineCount()-1 {
				return mi, nil, BoundaryBottom
			}
		}
	}
	var cmd tea.Cmd
	mi.ta, cmd = mi.ta.Update(msg)
	return mi, cmd, BoundaryNone
// §foot page/pkg/tui/multinput.go MultiInput.Update