// §head page/pkg/tui/settings_test.go:111-121 TestSettingsCursorStopsAtLastRow
// §sig func TestSettingsCursorStopsAtLastRow(t *testing.T)
	m := settingsModel(t)
	rows := len(m.settingRows())
	var mm tea.Model = m
	for i := 0; i < rows+3; i++ {
		mm, _ = mm.Update(tea.KeyMsg{Type: tea.KeyDown})
	}
	if got := mm.(Model).configCur; got != rows-1 {
		t.Errorf("configCur = %d after walking past the end, want %d", got, rows-1)
	}
// §foot page/pkg/tui/settings_test.go TestSettingsCursorStopsAtLastRow