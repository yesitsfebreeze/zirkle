// §head page/pkg/tui/tui.go:585-622 Model.relayout
// §sig func (m *Model) relayout()
	// Autocomplete is per-corpus, not per-feature: free text completes from the
	// prompt log, a search query from the pod fields search filters on.
	m.suggestion = ""
	if m.pane == 1 && !m.help && !m.config {
		switch m.mode {
		case modeNormal:
			m.suggestion = Suggest(m.history, m.input.Value())
		case modeSearch:
			m.suggestion = Suggest(m.searchCorpus(), m.searchQ)
		}
	}
	// Fixed rows outside the panes: pane-header (1) + status line (1) + any
	// user statusline scripts (above+below). The broadcast marquee adds one.
	chrome := 2 + m.scriptLineCount()
	if m.bc != nil {
		chrome++
	}
	inputH := 1
	if m.pane == 1 {
		lines := m.input.LineCount()
		if lines < 1 {
			lines = 1
		}
		maxH := max((m.winH-chrome)/3, 1) // up to 1/3 of screen
		inputH = min(lines, maxH)
	}
	m.input.SetHeight(inputH)
	m.input.SetGhost("")
	if m.suggestion != "" {
		m.input.SetGhost(m.suggestion)
	}
	avail := max(m.winH-chrome-inputH, 0)
	half := avail / 2
	m.vpTerminal.Height = half
	m.vpChat.Height = half
	m.vp.Height = avail - half
// §foot page/pkg/tui/tui.go Model.relayout