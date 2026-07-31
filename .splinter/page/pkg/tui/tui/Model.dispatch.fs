// §head page/pkg/tui/tui.go:371-439 Model.dispatch
// §sig func (m Model) dispatch(prompt string) (tea.Cmd, context.CancelFunc)
	if m.cmdr == nil {
		return func() tea.Msg { return doneRun{prompt: prompt, err: errNoCommander} }, nil
	}
	ctx, cancel := context.WithCancel(context.Background())

	if pc, ok := m.cmdr.(PlanCommander); ok {
		selectedID := ""
		selectedState := ""
		idx := m.selectedIdx()
		if idx >= 0 && idx < len(m.views) {
			selectedID = m.views[idx].ID
			selectedState = m.views[idx].State
		}

		if prompt == "approve" || prompt == ":approve" {
			if selectedID != "" && selectedID != "+ new" {
				return func() tea.Msg {
					_, err := pc.Approve(ctx, selectedID)
					if err != nil {
						return doneRun{prompt: prompt, err: err}
					}
					resp, err := pc.RunWorker(ctx, selectedID)
					return doneRun{prompt: prompt, response: resp, err: err}
				}, cancel
			}
		}

		if selectedID == "+ new" || selectedState == "created" || selectedState == "planning" {
			return func() tea.Msg {
				conv, err := pc.Plan(ctx, prompt)
				if err != nil {
					return doneRun{prompt: prompt, err: err}
				}
				return doneRun{prompt: prompt, response: "Plan updated: " + conv.Intent.Prompt, err: nil}
			}, cancel
		}

		if selectedState == "done" || selectedState == "failed" || selectedState == "waiting" {
			return func() tea.Msg {
				resp, err := pc.ReWork(ctx, selectedID, prompt)
				return doneRun{prompt: prompt, response: resp, err: err}
			}, cancel
		}
	}

	// Streaming path: when the commander supports it, stream tokens to the TUI.
	if sc, ok := m.cmdr.(StreamCommander); ok {
		events := make(chan llm.StreamEvent, 64)
		resultCh := make(chan doneRun, 1)
		go func() {
			resp, err := sc.RunStream(ctx, prompt, events)
			close(events)
			resultCh <- doneRun{prompt: prompt, response: resp, err: err}
		}()
		return func() tea.Msg {
			ev, ok := <-events
			if !ok {
				return <-resultCh
			}
			return agentStreamMsg{ev: ev, ch: events, resultCh: resultCh, prompt: prompt}
		}, cancel
	}

	return func() tea.Msg {
		resp, err := m.cmdr.Run(ctx, prompt)
		return doneRun{prompt: prompt, response: resp, err: err}
	}, cancel
// §foot page/pkg/tui/tui.go Model.dispatch