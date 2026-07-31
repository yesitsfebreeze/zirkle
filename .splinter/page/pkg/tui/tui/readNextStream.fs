// §head page/pkg/tui/tui.go:1069-1077 readNextStream
// §sig func readNextStream(ch <-chan llm.StreamEvent, resultCh <-chan doneRun, prompt string) tea.Cmd
	return func() tea.Msg {
		ev, ok := <-ch
		if !ok {
			return <-resultCh
		}
		return agentStreamMsg{ev: ev, ch: ch, resultCh: resultCh, prompt: prompt}
	}
// §foot page/pkg/tui/tui.go readNextStream