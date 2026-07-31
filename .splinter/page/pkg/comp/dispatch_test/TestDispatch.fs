// §head page/pkg/comp/dispatch_test.go:85-104 TestDispatch
// §sig func TestDispatch(t *testing.T)
	if runtime.GOOS == "windows" {
		t.Skip("just dispatch test not reliable on windows CI")
	}
	shard := &Shard{
		Name:     "echo-test",
		Run:      "echo-test",
		Justfile: "echo-test:\n    echo hello-from-shard\n",
	}
	out, code, err := Dispatch(shard, nil, nil)
	if err != nil {
		t.Skipf("just not available: %v", err)
	}
	if code != 0 {
		t.Errorf("exit code = %d, output: %q", code, out)
	}
	if !strings.Contains(out, "hello-from-shard") {
		t.Errorf("output missing expected text: %q", out)
	}
// §foot page/pkg/comp/dispatch_test.go TestDispatch