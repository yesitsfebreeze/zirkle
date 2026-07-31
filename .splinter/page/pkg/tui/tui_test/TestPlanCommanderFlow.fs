// §head page/pkg/tui/tui_test.go:610-670 TestPlanCommanderFlow
// §sig func TestPlanCommanderFlow(t *testing.T)
	pc := &mockPlanCommander{}
	views := []PodView{
		{ID: "+ new", State: "ready"},
		{ID: "plan-1", State: "planning"},
		{ID: "plan-2", State: "done"},
	}
	m := New(mockSource{views: views}, pc, nil)
	next, _ := m.Update(tea.WindowSizeMsg{Width: 80, Height: 24})
	mm := next.(Model)
	next, _ = mm.Update(refreshMsg(views))
	mm = next.(Model)

	// Helper to move cursor to a specific pod ID in visible rows
	selectPod := func(id string) {
		for i, idx := range mm.visible() {
			if mm.views[idx].ID == id {
				mm.cursor = i
				return
			}
		}
		t.Fatalf("pod %s not found in visible views", id)
	}

	// 1. Submit on + new triggers Plan
	selectPod("+ new")
	mm.input.SetValue("new feature idea")
	next, cmd := mm.submitInput()
	mm = next.(Model)
	if cmd != nil {
		cmd()
	}
	if !pc.planCalled {
		t.Error("expected pc.Plan to be called for + new input")
	}

	// 2. Submit :approve on plan-1 triggers Approve & RunWorker
	pc.planCalled = false
	selectPod("plan-1")
	mm.input.SetValue(":approve")
	next, cmd = mm.submitInput()
	mm = next.(Model)
	if cmd != nil {
		cmd()
	}
	if !pc.approveCalled {
		t.Error("expected pc.Approve to be called for :approve command")
	}

	// 3. Submit correction on plan-2 triggers ReWork
	selectPod("plan-2")
	mm.input.SetValue("make header bigger")
	next, cmd = mm.submitInput()
	mm = next.(Model)
	if cmd != nil {
		cmd()
	}
	if !pc.reworkCalled {
		t.Error("expected pc.ReWork to be called for correction input on done plan")
	}
// §foot page/pkg/tui/tui_test.go TestPlanCommanderFlow