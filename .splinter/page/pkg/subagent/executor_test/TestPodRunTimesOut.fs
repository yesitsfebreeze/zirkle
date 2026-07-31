// §head page/pkg/subagent/executor_test.go:180-200 TestPodRunTimesOut
// §sig func TestPodRunTimesOut(t *testing.T)
	o := Pod{
		Host:    "pod-1",
		Command: shim(t, `sleep 5`),
	}

	start := time.Now()
	res, err := o.Run(context.Background(), Config{Prompt: "slow one", Timeout: 150 * time.Millisecond})
	if err != nil {
		t.Fatalf("Pod.Run: %v", err)
	}
	if res.Success {
		t.Fatal("expected timeout failure")
	}
	if res.Summary != "subagent timed out" {
		t.Fatalf("summary: got %q", res.Summary)
	}
	if elapsed := time.Since(start); elapsed > 3*time.Second {
		t.Fatalf("timeout not honoured: took %v", elapsed)
	}
// §foot page/pkg/subagent/executor_test.go TestPodRunTimesOut