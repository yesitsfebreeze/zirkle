// §head page/pkg/config/save_test.go:26-74 TestSaveTimelinePatchesOnlyItsSection
// §sig func TestSaveTimelinePatchesOnlyItsSection(t *testing.T)
	path := filepath.Join(t.TempDir(), "config.toml")
	if err := os.WriteFile(path, []byte(userFile), 0o644); err != nil {
		t.Fatal(err)
	}

	tl := TimelineConfig{Enabled: false, Frame: "week", DayStart: "04:00", ShowCount: true}
	if err := SaveTimeline(path, tl); err != nil {
		t.Fatal(err)
	}

	out, err := os.ReadFile(path)
	if err != nil {
		t.Fatal(err)
	}
	got := string(out)
	for _, want := range []string{
		"# my config, my comments",
		"port = 9842      # do not touch",
		`socket = "/tmp/relay.sock"`,
		"[log]",
		`level = "debug"`,
		"enabled = false",
		`frame = "week"`,
		`day_start = "04:00"`,
		"show_count = true",
		"show_states = false",
		"show_span = false",
	} {
		if !strings.Contains(got, want) {
			t.Errorf("saved file missing %q:\n%s", want, got)
		}
	}
	if strings.Contains(got, "stale comment") {
		t.Errorf("comment inside [timeline] should be replaced with the block:\n%s", got)
	}
	if n := strings.Count(got, "[timeline]"); n != 1 {
		t.Errorf("[timeline] appears %d times, want 1:\n%s", n, got)
	}

	// Round-trip: what was written is what Load reads back.
	c, err := Load(path)
	if err != nil {
		t.Fatal(err)
	}
	if c.Timeline != tl {
		t.Errorf("loaded %+v, want %+v", c.Timeline, tl)
	}
// §foot page/pkg/config/save_test.go TestSaveTimelinePatchesOnlyItsSection