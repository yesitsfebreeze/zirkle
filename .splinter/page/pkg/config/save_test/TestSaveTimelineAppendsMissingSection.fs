// §head page/pkg/config/save_test.go:77-96 TestSaveTimelineAppendsMissingSection
// §sig func TestSaveTimelineAppendsMissingSection(t *testing.T)
	path := filepath.Join(t.TempDir(), "config.toml")
	if err := os.WriteFile(path, []byte("[log]\nlevel = \"info\"\n"), 0o644); err != nil {
		t.Fatal(err)
	}

	if err := SaveTimeline(path, TimelineConfig{Enabled: true, Frame: "hour", DayStart: "00:00", ShowSpan: true}); err != nil {
		t.Fatal(err)
	}
	c, err := Load(path)
	if err != nil {
		t.Fatal(err)
	}
	if c.Timeline.Frame != "hour" || !c.Timeline.ShowSpan || c.Timeline.ShowCount {
		t.Errorf("appended section read back as %+v", c.Timeline)
	}
	if c.Log.Level != "info" {
		t.Errorf("appending clobbered [log]: level = %q", c.Log.Level)
	}
// §foot page/pkg/config/save_test.go TestSaveTimelineAppendsMissingSection