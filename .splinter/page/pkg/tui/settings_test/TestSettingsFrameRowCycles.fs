// §head page/pkg/tui/settings_test.go:95-108 TestSettingsFrameRowCycles
// §sig func TestSettingsFrameRowCycles(t *testing.T)
	m := settingsModel(t)
	m.configCur = 6

	next, _ := m.Update(tea.KeyMsg{Type: tea.KeyRight})
	mm := next.(Model)
	if mm.tl.Frame != "week" {
		t.Errorf("frame after right = %q, want week", mm.tl.Frame)
	}
	next, _ = mm.Update(tea.KeyMsg{Type: tea.KeyLeft})
	if f := next.(Model).tl.Frame; f != "day" {
		t.Errorf("frame after left = %q, want day", f)
	}
// §foot page/pkg/tui/settings_test.go TestSettingsFrameRowCycles