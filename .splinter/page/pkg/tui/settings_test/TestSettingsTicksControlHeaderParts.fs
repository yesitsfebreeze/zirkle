// §head page/pkg/tui/settings_test.go:124-148 TestSettingsTicksControlHeaderParts
// §sig func TestSettingsTicksControlHeaderParts(t *testing.T)
	m := settingsModel(t)

	full := m.renderTree()
	if !strings.Contains(full, "pods") {
		t.Fatalf("header missing the pod count with every tick on:\n%s", full)
	}

	m.tl.ShowCount = false
	if out := m.renderTree(); strings.Contains(out, "pods") {
		t.Errorf("pod count still rendered with the tick off:\n%s", out)
	}
	m.tl.ShowStates = false
	if out := m.renderTree(); strings.Contains(out, "1■") || strings.Contains(out, "1✕") {
		t.Errorf("state tallies still rendered with the tick off:\n%s", out)
	}
	m.tl.ShowSpan = false
	if out := m.renderTree(); strings.Contains(out, "span ") {
		t.Errorf("span still rendered with the tick off:\n%s", out)
	}
	// The bare label survives: the row is still a frame boundary.
	if out := m.renderTree(); !strings.Contains(out, "today") {
		t.Errorf("frame label lost when every part is unticked:\n%s", out)
	}
// §foot page/pkg/tui/settings_test.go TestSettingsTicksControlHeaderParts