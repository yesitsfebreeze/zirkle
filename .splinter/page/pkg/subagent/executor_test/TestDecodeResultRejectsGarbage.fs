// §head page/pkg/subagent/executor_test.go:202-206 TestDecodeResultRejectsGarbage
// §sig func TestDecodeResultRejectsGarbage(t *testing.T)
	if _, err := decodeResult([]byte("no json here\n")); err == nil {
		t.Fatal("expected error")
	}
// §foot page/pkg/subagent/executor_test.go TestDecodeResultRejectsGarbage