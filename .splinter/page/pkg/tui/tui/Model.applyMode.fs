// §head page/pkg/tui/tui.go:186-200 Model.applyMode
// §sig func (m *Model) applyMode(mode inputMode)
	m.mode = mode
	m.input.Reset()
	m.searchQ = ""
	m.suggestion = ""
	m.histIdx = -1
	m.search = mode == modeSearch
	m.help = mode == modeHelp
	if m.help {
		m.helpCur = 0
		m.helpDetail = -1
	}
	m.pane = 1
	m.input.Focus()
// §foot page/pkg/tui/tui.go Model.applyMode