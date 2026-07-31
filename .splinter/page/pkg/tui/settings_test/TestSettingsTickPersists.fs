// §head page/pkg/tui/settings_test.go:58-80 TestSettingsTickPersists
// §sig func TestSettingsTickPersists(t *testing.T)
	m := settingsModel(t)
	var got TimelineConfig
	calls := 0
	m.tlSave = func(c TimelineConfig) error {
		got = c
		calls++
		return nil
	}
	m.configCur = 4 // State Tallies

	next, _ := m.Update(tea.KeyMsg{Type: tea.KeyRunes, Runes: []rune{' '}})
	mm := next.(Model)
	if calls != 1 {
		t.Fatalf("save called %d times, want 1", calls)
	}
	if got.ShowStates {
		t.Error("persisted config still has state tallies ticked")
	}
	if mm.err != "" {
		t.Errorf("unexpected error surfaced: %q", mm.err)
	}
// §foot page/pkg/tui/settings_test.go TestSettingsTickPersists