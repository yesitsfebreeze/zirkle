// §head page/pkg/subagent/executor_test.go:98-102 TestPodRunNeedsHost
// §sig func TestPodRunNeedsHost(t *testing.T)
	if _, err := (Pod{}).Run(context.Background(), Config{Prompt: "x"}); err == nil {
		t.Fatal("expected error for empty host")
	}
// §foot page/pkg/subagent/executor_test.go TestPodRunNeedsHost