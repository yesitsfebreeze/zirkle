// §head page/pkg/tui/tui.go:813-829 Model.loadConversation
// §sig func (m Model) loadConversation() tea.Cmd
	if m.streaming || m.busy {
		return nil
	}
	id := m.selectedID()
	if id == "" || id == m.loadedID {
		return nil
	}
	cs, ok := m.src.(ConversationSource)
	if !ok {
		return nil
	}
	return func() tea.Msg {
		msgs, err := cs.Conversation(id)
		return conversationMsg{id: id, msgs: msgs, err: err}
	}
// §foot page/pkg/tui/tui.go Model.loadConversation