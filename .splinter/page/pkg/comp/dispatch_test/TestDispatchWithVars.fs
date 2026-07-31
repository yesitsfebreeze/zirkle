// §head page/pkg/comp/dispatch_test.go:106-125 TestDispatchWithVars
// §sig func TestDispatchWithVars(t *testing.T)
	if runtime.GOOS == "windows" {
		t.Skip("just dispatch test not reliable on windows CI")
	}
	shard := &Shard{
		Name: "var-test",
		Run:  "var-test",
		Justfile: "var-test:\n    echo <<msg>>\n",
	}
	out, code, err := Dispatch(shard, map[string]string{"msg": "injected"}, nil)
	if err != nil {
		t.Skipf("just not available: %v", err)
	}
	if code != 0 {
		t.Errorf("exit code = %d, output: %q", code, out)
	}
	if !strings.Contains(out, "injected") {
		t.Errorf("var injection failed: %q", out)
	}
// §foot page/pkg/comp/dispatch_test.go TestDispatchWithVars