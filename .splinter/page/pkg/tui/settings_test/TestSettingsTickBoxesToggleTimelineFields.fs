// §head page/pkg/tui/settings_test.go:29-55 TestSettingsTickBoxesToggleTimelineFields
// §sig func TestSettingsTickBoxesToggleTimelineFields(t *testing.T)
	for _, tc := range []struct {
		row  int
		name string
		get  func(TimelineConfig) bool
	}{
		{2, "Timeline Headers", func(c TimelineConfig) bool { return c.Enabled }},
		{3, "Pod Count", func(c TimelineConfig) bool { return c.ShowCount }},
		{4, "State Tallies", func(c TimelineConfig) bool { return c.ShowStates }},
		{5, "Time Span", func(c TimelineConfig) bool { return c.ShowSpan }},
	} {
		m := settingsModel(t)
		m.configCur = tc.row
		if !tc.get(m.tl) {
			t.Fatalf("%s: default must start ticked", tc.name)
		}
		next, _ := m.Update(tea.KeyMsg{Type: tea.KeyRunes, Runes: []rune{' '}})
		mm := next.(Model)
		if tc.get(mm.tl) {
			t.Errorf("%s: space did not untick it", tc.name)
		}
		next, _ = mm.Update(tea.KeyMsg{Type: tea.KeyRunes, Runes: []rune{' '}})
		if !tc.get(next.(Model).tl) {
			t.Errorf("%s: space did not tick it back", tc.name)
		}
	}
// §foot page/pkg/tui/settings_test.go TestSettingsTickBoxesToggleTimelineFields