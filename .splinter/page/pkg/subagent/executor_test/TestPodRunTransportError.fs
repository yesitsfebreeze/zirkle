// §head page/pkg/subagent/executor_test.go:169-178 TestPodRunTransportError
// §sig func TestPodRunTransportError(t *testing.T)
	o := Pod{
		Host:    "pod-1",
		Command: shim(t, `echo "ssh: connect to host pod-1: Connection refused" >&2; exit 255`),
	}

	if _, err := o.Run(context.Background(), Config{Prompt: "x", Timeout: 5 * time.Second}); err == nil {
		t.Fatal("expected transport error")
	}
// §foot page/pkg/subagent/executor_test.go TestPodRunTransportError