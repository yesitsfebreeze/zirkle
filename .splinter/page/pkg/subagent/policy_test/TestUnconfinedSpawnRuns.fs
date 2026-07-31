// §head page/pkg/subagent/policy_test.go:58-73 TestUnconfinedSpawnRuns
// §sig func TestUnconfinedSpawnRuns(t *testing.T)
	t.Setenv(EnvSandbox, "off")
	t.Setenv("RELAY_SUBAGENT_RUN", "1")

	res, err := Spawn(context.Background(), Config{
		Prompt:   "do something",
		ParentID: "test-parent",
		Timeout:  5 * time.Second,
	})
	if err != nil {
		t.Fatalf("Spawn: %v", err)
	}
	if res.Summary != "test summary" {
		t.Fatalf("summary: got %q", res.Summary)
	}
// §foot page/pkg/subagent/policy_test.go TestUnconfinedSpawnRuns