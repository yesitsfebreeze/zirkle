// §head page/pkg/tui/tui.go:1080-1116 Model.afterCursorMove
// §sig func (m Model) afterCursorMove() (tea.Model, tea.Cmd)
	m.clampCursor()
	m.updatePrompt()
	m.relayout()
	if m.pane == 0 {
		if m.cursor < m.vp.YOffset {
			m.vp.YOffset = m.cursor
		}
		if m.cursor >= m.vp.YOffset+m.vp.Height {
			m.vp.YOffset = m.cursor - m.vp.Height + 1
		}
	}
	// Selecting a different pod loads its persisted conversation into the chat
	// pane. Set loadedID optimistically so a refresh before the response lands
	// doesn't re-fire; the response is dropped if the selection moved on.
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
		} else if id == "" {
			m.loadedID = "" // moved to "+ new" / nothing: forget the last pod
		}
	}
	if m.ready {
		m.vp.SetContent(m.treeContent())
		m.vpChat.SetContent(m.thoughtsContent())
		m.vpTerminal.SetContent(m.terminalContent())
	}
	return m, lcmd
// §foot page/pkg/tui/tui.go Model.afterCursorMove