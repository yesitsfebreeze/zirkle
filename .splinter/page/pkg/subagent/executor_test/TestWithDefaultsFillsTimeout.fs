// §head page/pkg/subagent/executor_test.go:208-215 TestWithDefaultsFillsTimeout
// §sig func TestWithDefaultsFillsTimeout(t *testing.T)
	if got := (Config{}).withDefaults().Timeout; got != 60*time.Second {
		t.Fatalf("timeout: got %v, want 60s", got)
	}
	if got := (Config{Timeout: time.Second}).withDefaults().Timeout; got != time.Second {
		t.Fatalf("timeout overwritten: got %v", got)
	}
// §foot page/pkg/subagent/executor_test.go TestWithDefaultsFillsTimeout