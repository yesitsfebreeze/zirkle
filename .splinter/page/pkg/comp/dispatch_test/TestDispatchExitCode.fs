// §head page/pkg/comp/dispatch_test.go:127-143 TestDispatchExitCode
// §sig func TestDispatchExitCode(t *testing.T)
	if runtime.GOOS == "windows" {
		t.Skip("just dispatch test not reliable on windows CI")
	}
	shard := &Shard{
		Name: "fail-test",
		Run:  "fail-test",
		Justfile: "fail-test:\n    exit 42\n",
	}
	_, code, err := Dispatch(shard, nil, nil)
	if err != nil {
		t.Skipf("just not available: %v", err)
	}
	if code != 42 {
		t.Errorf("expected exit 42, got %d", code)
	}
// §foot page/pkg/comp/dispatch_test.go TestDispatchExitCode