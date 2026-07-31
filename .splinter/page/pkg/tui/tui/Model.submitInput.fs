// §head page/pkg/tui/tui.go:471-507 Model.submitInput
// §sig func (m Model) submitInput() (tea.Model, tea.Cmd)
	prompt := strings.TrimSpace(m.input.Value())
	if prompt == "" {
		return m, nil
	}
	m.applyMode(modeNormal) // submitting ends the mode: clear and restart

	if mdl, cmd, handled := m.runCommand(strings.Fields(prompt)); handled {
		return mdl, cmd
	}
	m.busy = true
	m.streaming = true
	m.thoughts = ""
	// The prompt lives in the chat log (left pane); the terminal pane shows
	// subpod/shell output only, so we don't echo the prompt there.
	m.history = append([]string{prompt}, m.history...)
	if len(m.history) > historyLimit {
		m.history = m.history[:historyLimit]
	}
	m.histIdx = -1
	m.suggestion = ""
	m.chat = append(m.chat, ChatMsg{Role: "user", Content: prompt})
	if m.ready {
		m.vpTerminal.SetContent(m.terminalContent())
		m.vpTerminal.GotoBottom()
		m.vpChat.SetContent(m.thoughtsContent())
		m.vpChat.GotoBottom()
	}
	if m.hist != nil {
		cmd, cancel := m.dispatch(prompt)
		m.cancelFn = cancel
		return m, tea.Batch(cmd, m.recordPrompt(prompt))
	}
	cmd, cancel := m.dispatch(prompt)
	m.cancelFn = cancel
	return m, cmd
// §foot page/pkg/tui/tui.go Model.submitInput