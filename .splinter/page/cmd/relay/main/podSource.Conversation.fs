// §head page/cmd/relay/main.go:111-136 podSource.Conversation
// §sig func (s *podSource) Conversation(id string) ([]tui.ChatMsg, error)
	if strings.HasPrefix(id, "subpod:") {
		return nil, nil
	}
	state, err := s.store.LatestCheckpoint(id)
	if err != nil {
		return nil, err
	}
	if len(state) == 0 {
		return nil, nil
	}
	var msgs []llm.Message
	if err := json.Unmarshal(state, &msgs); err != nil {
		return nil, err
	}
	out := make([]tui.ChatMsg, 0, len(msgs))
	for _, mm := range msgs {
		switch {
		case mm.Role == "assistant" && strings.TrimSpace(mm.Content) != "":
			out = append(out, tui.ChatMsg{Role: "agent", Content: mm.Content})
		case mm.Role == "user" && mm.ToolResult == nil && strings.TrimSpace(mm.Content) != "":
			out = append(out, tui.ChatMsg{Role: "user", Content: mm.Content})
		}
	}
	return out, nil
// §foot page/cmd/relay/main.go podSource.Conversation