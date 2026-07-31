// §head page/pkg/tui/tui_test.go:324-334 TestDoneRunClearsBusy
// §sig func TestDoneRunClearsBusy(t *testing.T)
	m := New(mockSource{views: nil}, &recordingCommander{}, nil)
	next, _ := m.Update(tea.WindowSizeMsg{Width: 80, Height: 24})
	mm := next.(Model)
	mm.busy = true

	next, _ = mm.Update(doneRun{prompt: "p", response: "r"})
	if next.(Model).busy {
		t.Error("busy still set after doneRun — input would stay locked")
	}
// §foot page/pkg/tui/tui_test.go TestDoneRunClearsBusy