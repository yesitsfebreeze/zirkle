// §head page/pkg/tui/settings_test.go:83-92 TestSettingsTickReportsSaveFailure
// §sig func TestSettingsTickReportsSaveFailure(t *testing.T)
	m := settingsModel(t)
	m.tlSave = func(TimelineConfig) error { return errFake }
	m.configCur = 2

	next, _ := m.Update(tea.KeyMsg{Type: tea.KeyRunes, Runes: []rune{' '}})
	if mm := next.(Model); mm.err != errFake.Error() {
		t.Errorf("err = %q, want %q", mm.err, errFake.Error())
	}
// §foot page/pkg/tui/settings_test.go TestSettingsTickReportsSaveFailure