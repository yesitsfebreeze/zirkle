// §head page/pkg/cli/cli_test.go:243-248 TestDialError
// §sig func TestDialError(t *testing.T)
	c := New("/nonexistent/sock")
	if err := c.Dial(); err == nil {
		t.Fatal("expected dial error")
	}
// §foot page/pkg/cli/cli_test.go TestDialError