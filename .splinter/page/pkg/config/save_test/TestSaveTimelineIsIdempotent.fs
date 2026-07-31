// §head page/pkg/config/save_test.go:99-117 TestSaveTimelineIsIdempotent
// §sig func TestSaveTimelineIsIdempotent(t *testing.T)
	path := filepath.Join(t.TempDir(), "config.toml")
	if err := os.WriteFile(path, []byte(userFile), 0o644); err != nil {
		t.Fatal(err)
	}
	tl := TimelineConfig{Enabled: true, Frame: "month", DayStart: "00:00", ShowStates: true}
	for i := 0; i < 3; i++ {
		if err := SaveTimeline(path, tl); err != nil {
			t.Fatalf("save #%d: %v", i, err)
		}
	}
	out, _ := os.ReadFile(path)
	if n := strings.Count(string(out), "[timeline]"); n != 1 {
		t.Errorf("[timeline] appears %d times after 3 saves:\n%s", n, out)
	}
	if n := strings.Count(string(out), "show_states"); n != 1 {
		t.Errorf("show_states appears %d times after 3 saves", n)
	}
// §foot page/pkg/config/save_test.go TestSaveTimelineIsIdempotent