// §head page/pkg/store/store_test.go:319-350 TestPromptHistory
// §sig func TestPromptHistory(t *testing.T)
	s, err := Open(filepath.Join(t.TempDir(), "test.db"))
	if err != nil {
		t.Fatal(err)
	}
	defer s.Close()

	if err := s.RecordPrompt("  "); err != nil {
		t.Fatal(err)
	}
	for _, p := range []string{"deploy staging", "deploy prod", "deploy prod"} {
		if err := s.RecordPrompt(p); err != nil {
			t.Fatal(err)
		}
	}
	got, err := s.RecentPrompts(10)
	if err != nil {
		t.Fatal(err)
	}
	want := []string{"deploy prod", "deploy prod", "deploy staging"}
	if len(got) != len(want) {
		t.Fatalf("prompts = %v, want %v", got, want)
	}
	for i := range want {
		if got[i] != want[i] {
			t.Fatalf("prompts = %v, want %v (newest first, duplicates kept)", got, want)
		}
	}
	if got, err = s.RecentPrompts(1); err != nil || len(got) != 1 {
		t.Fatalf("limit ignored: %v %v", got, err)
	}
// §foot page/pkg/store/store_test.go TestPromptHistory