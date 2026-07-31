// §head page/pkg/tui/tui.go:892-1063 Model.Update
// §sig func (m Model) Update(msg tea.Msg) (tea.Model, tea.Cmd)
	switch msg := msg.(type) {
	case tea.WindowSizeMsg:
		m.winH = msg.Height
		totalW := max(msg.Width, 0)
		divW := 1
		halfW := max(totalW/2-divW/2, 1)
		m.vpTerminal.Width = halfW
		m.vpChat.Width = max(totalW-halfW-divW, 1)
		m.vp.Width = totalW
		m.input.SetWidth(max(totalW-5, 0)) // -5: leave room for scrollbar
		m.relayout()
		m.updatePrompt()
		m.ready = true
		m.vpTerminal.SetContent(m.terminalContent())
		m.vpChat.SetContent(m.thoughtsContent())
		m.vp.SetContent(m.treeContent())
		m.vpChat.GotoBottom()
		m.vp.GotoTop()
		return m, m.load()
	case errMsg:
		m.err = msg.Error()
		return m, nil
	case promptsMsg:
		m.history = append(m.history, msg...)
		if len(m.history) > historyLimit {
			m.history = m.history[:historyLimit]
		}
		return m, nil
	case refreshMsg:
		atTop := m.cursor <= 0
		m.views = reverseGroups(msg)
		m.err = ""
		if atTop {
			m.cursor = 0
		}
		m.clampCursor()
		if m.detailIdx() < 0 {
			m.detail = ""
		}
		m.updatePrompt()
		if m.ready {
			m.vpTerminal.SetContent(m.terminalContent())
			m.vpChat.SetContent(m.thoughtsContent())
			m.vp.SetContent(m.treeContent())
			m.vpChat.GotoBottom()
		}
		// First refresh with a real pod selected: load its conversation once.
		var lcmd tea.Cmd
		if !m.streaming && !m.busy {
			if id := m.selectedID(); id != "" && id != m.loadedID {
				m.loadedID = id
				if cs, ok := m.src.(ConversationSource); ok {
					id := id
					lcmd = func() tea.Msg {
						msgs, err := cs.Conversation(id)
						return conversationMsg{id: id, msgs: msgs, err: err}
					}
				}
			}
		}
		return m, lcmd
	case statuslineMsg:
		m.scriptLines = msg
		m.relayout()
		return m, nil
	case conversationMsg:
		idx := m.selectedIdx()
		if idx >= 0 && idx < len(m.views) && m.views[idx].ID == msg.id {
			if msg.err != nil {
				m.err = msg.err.Error()
			} else {
				m.chat = msg.msgs
			}
			if m.ready {
				m.vpChat.SetContent(m.thoughtsContent())
				m.vpChat.GotoBottom()
			}
		}
		return m, nil
	case doneRun:
		m.busy = false
		m.streaming = false
		m.cancelFn = nil
		m.detail = ""
		if msg.err != nil {
			m.err = msg.err.Error()
			m.terminal = append(m.terminal, footerStyle.Render(" error: "+msg.err.Error()))
			m.chat = append(m.chat, ChatMsg{Role: "agent", Content: "error: " + msg.err.Error()})
		} else if msg.response != "" {
			m.chat = append(m.chat, ChatMsg{Role: "agent", Content: msg.response})
			m.terminal = append(m.terminal, activeStyle.Render(" ✓ done"))
		}
		if m.ready {
			m.vpTerminal.SetContent(m.terminalContent())
			m.vpChat.SetContent(m.thoughtsContent())
			m.vpTerminal.GotoBottom()
			m.vpChat.GotoBottom()
		}
		return m, m.load()
	case agentStreamMsg:
		if msg.ev.Err != nil {
			m.streaming = false
			m.busy = false
			m.terminal = append(m.terminal, footerStyle.Render(" error: "+msg.ev.Err.Error()))
			if m.ready {
				m.vpTerminal.SetContent(m.terminalContent())
				m.vpTerminal.GotoBottom()
			}
			return m, m.load()
		}
		if msg.ev.ToolCall != nil {
			prompt := ""
			if p, ok := msg.ev.ToolCall.Input["prompt"].(string); ok {
				prompt = p
			}
			m.terminal = append(m.terminal, activeStyle.Render(" [tool] "+msg.ev.ToolCall.Name)+" "+trunc(prompt, m.vpTerminal.Width-10))
		}
		if msg.ev.ToolOutput != "" {
			// Tool/shell result → right pane (terminal output), one line per row.
			for _, line := range strings.Split(strings.TrimRight(msg.ev.ToolOutput, "\n"), "\n") {
				m.terminal = append(m.terminal, " "+trunc(line, m.vpTerminal.Width-2))
			}
		}
		if msg.ev.Content != "" {
			m.thoughts += msg.ev.Content
		}
		if msg.ev.Done {
			// Stream complete — continue the chain so readNextStream reads the
			// doneRun from resultCh after the channel closes.
			return m, readNextStream(msg.ch, msg.resultCh, msg.prompt)
		}
		if m.ready {
			m.vpChat.SetContent(m.thoughtsContent())
			m.vpChat.GotoBottom()
			m.vpTerminal.SetContent(m.terminalContent())
			m.vpTerminal.GotoBottom()
		}
		return m, readNextStream(msg.ch, msg.resultCh, msg.prompt)
	case tea.KeyMsg:
		return m.handleKey(msg)
	case wizardTickMsg:
		return m, nil
	case tickMsg:
		m.statTime = time.Time(msg).Format("15:04:05")
		m.statLoad = readLoad()
		return m, tea.Batch(tick(), m.load(), m.collectStatuslines())
	case broadcastTickMsg:
		if m.broadcasts != nil {
			select {
			case text := <-m.broadcasts:
				if text = strings.TrimSpace(text); text != "" {
					m.bc = &broadcastMsg{text: text, pos: m.vpChat.Width + m.vpTerminal.Width}
					return m, scrollTick()
				}
			default:
			}
		}
		return m, broadcastTick()
	case scrollTickMsg:
		if m.bc != nil {
			m.bc.pos--
			if m.bc.pos < -len([]rune(m.bc.text)) {
				m.bc = nil
				return m, broadcastTick()
			}
			return m, scrollTick()
		}
		return m, nil
	}
	return m, nil
// §foot page/pkg/tui/tui.go Model.Update