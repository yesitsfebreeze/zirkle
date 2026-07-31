// §head page/pkg/tui/settings_test.go:151-164 TestSettingsTicksLeaveSelectionAlone
// §sig func TestSettingsTicksLeaveSelectionAlone(t *testing.T)
	m := settingsModel(t)
	m.cursor = 1
	m.configCur = 2

	next, _ := m.Update(tea.KeyMsg{Type: tea.KeyRunes, Runes: []rune{' '}})
	mm := next.(Model)
	if id := mm.selectedID(); id != "p1" {
		t.Errorf("selected %q after a tick, want p1", id)
	}
	if n := len(mm.visible()); n != 2 {
		t.Errorf("visible rows = %d, want 2 pods and no headers", n)
	}
// §foot page/pkg/tui/settings_test.go TestSettingsTicksLeaveSelectionAlone