// §head page/pkg/store/store_test.go:383-438 TestExecutionRecordAndSearch
// §sig func TestExecutionRecordAndSearch(t *testing.T)
	s, err := Open(filepath.Join(t.TempDir(), "t.db"))
	if err != nil {
		t.Fatalf("Open: %v", err)
	}

	runs := []*Execution{
		{ParentID: "planner", Prompt: "check disk usage", Summary: "disk at 22%", Output: "df output here", Success: true, Tokens: 50},
		{ParentID: "planner", Prompt: "search process pods", Summary: "no pods found", Output: "search results", Success: false, Tokens: 80},
		{ParentID: "planner", Prompt: "check cpu load", Summary: "cpu at 12%", Output: "top output", Success: true, Tokens: 40},
	}
	for _, e := range runs {
		if err := s.RecordExecution(e); err != nil {
			t.Fatalf("RecordExecution: %v", err)
		}
	}

	// Newest first.
	recent, err := s.RecentExecutions(10)
	if err != nil {
		t.Fatalf("RecentExecutions: %v", err)
	}
	if len(recent) != 3 {
		t.Fatalf("got %d executions, want 3", len(recent))
	}
	if recent[0].Prompt != "check cpu load" {
		t.Fatalf("newest first: got %q", recent[0].Prompt)
	}
	if !recent[0].Success {
		t.Fatal("success flag lost")
	}

	// Search hits prompt, summary, and output.
	for query, want := range map[string]string{
		"disk":       "check disk usage",
		"no pods":    "search process pods",
		"top output": "check cpu load",
	} {
		hits, err := s.SearchExecutions(query, 10)
		if err != nil {
			t.Fatalf("SearchExecutions(%q): %v", query, err)
		}
		if len(hits) != 1 || hits[0].Prompt != want {
			t.Fatalf("search %q: got %+v, want prompt %q", query, hits, want)
		}
	}

	// No match → empty, not error.
	hits, err := s.SearchExecutions("nonexistent-xyzzy", 10)
	if err != nil {
		t.Fatalf("SearchExecutions: %v", err)
	}
	if len(hits) != 0 {
		t.Fatalf("expected 0 hits, got %d", len(hits))
	}
// §foot page/pkg/store/store_test.go TestExecutionRecordAndSearch