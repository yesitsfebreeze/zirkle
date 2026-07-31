// §head page/pkg/tui/settings_test.go:167-177 TestSettingsScreenRendersTickBoxes
// §sig func TestSettingsScreenRendersTickBoxes(t *testing.T)
	m := settingsModel(t)
	m.tl.ShowSpan = false

	out := m.renderConfig()
	for _, want := range []string{"Timeline Headers", "[x]", "[ ]", "< day >", "Space toggle"} {
		if !strings.Contains(out, want) {
			t.Errorf("settings screen missing %q:\n%s", want, out)
		}
	}
// §foot page/pkg/tui/settings_test.go TestSettingsScreenRendersTickBoxes