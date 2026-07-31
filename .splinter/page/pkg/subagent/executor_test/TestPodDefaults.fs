// §head page/pkg/subagent/executor_test.go:88-96 TestPodDefaults
// §sig func TestPodDefaults(t *testing.T)
	var o Pod
	if o.command() != "ssh" {
		t.Fatalf("command: got %q, want ssh", o.command())
	}
	if o.binary() != "relay" {
		t.Fatalf("binary: got %q, want relay", o.binary())
	}
// §foot page/pkg/subagent/executor_test.go TestPodDefaults