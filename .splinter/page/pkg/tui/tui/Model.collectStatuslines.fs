// §head page/pkg/tui/tui.go:835-853 Model.collectStatuslines
// §sig func (m Model) collectStatuslines() tea.Cmd
	dir := statusLineDir()
	if dir == "" {
		return nil
	}
	env := map[string]string{
		"RELAY_VERSION":   Version,
		"RELAY_PODS":      strconv.Itoa(len(m.views)),
		"RELAY_RUNNING":   strconv.Itoa(m.runningCount()),
		"RELAY_ACTIVE":    strconv.Itoa(m.activeCount()),
		"RELAY_LOAD":      m.statLoad,
		"RELAY_TIME":      m.statTime,
		"RELAY_BUSY":      boolStr(m.busy),
		"RELAY_PANE":      strconv.Itoa(m.pane),
		"RELAY_SEL_ID":    m.selectedID(),
		"RELAY_SEL_STATE": selectedState(m),
	}
	return func() tea.Msg { return statuslineMsg(CollectScriptLines(dir, env)) }
// §foot page/pkg/tui/tui.go Model.collectStatuslines