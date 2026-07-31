// §head page/pkg/store/store_test.go:352-379 TestPromptHistoryTrimsToLimit
// §sig func TestPromptHistoryTrimsToLimit(t *testing.T)
	s, err := Open(filepath.Join(t.TempDir(), "test.db"))
	if err != nil {
		t.Fatal(err)
	}
	defer s.Close()

	for i := 0; i < PromptHistoryLimit+20; i++ {
		if err := s.RecordPrompt(fmt.Sprintf("p%d", i)); err != nil {
			t.Fatal(err)
		}
	}
	got, err := s.RecentPrompts(PromptHistoryLimit)
	if err != nil {
		t.Fatal(err)
	}
	if len(got) != PromptHistoryLimit {
		t.Fatalf("kept %d rows, want %d", len(got), PromptHistoryLimit)
	}
	newest := fmt.Sprintf("p%d", PromptHistoryLimit+19)
	if got[0] != newest {
		t.Fatalf("newest = %q, want %q", got[0], newest)
	}
	oldest := fmt.Sprintf("p%d", 20)
	if got[len(got)-1] != oldest {
		t.Fatalf("oldest kept = %q, want %q", got[len(got)-1], oldest)
	}
// §foot page/pkg/store/store_test.go TestPromptHistoryTrimsToLimit