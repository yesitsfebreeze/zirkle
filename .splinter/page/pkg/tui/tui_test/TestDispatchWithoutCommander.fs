// §head page/pkg/tui/tui_test.go:401-412 TestDispatchWithoutCommander
// §sig func TestDispatchWithoutCommander(t *testing.T)
	m := New(mockSource{views: nil}, nil, nil)
	cmd, _ := m.dispatch("x")
	msg := cmd()
	done, ok := msg.(doneRun)
	if !ok {
		t.Fatalf("got %T, want doneRun", msg)
	}
	if done.err == nil {
		t.Error("nil commander produced no error")
	}
// §foot page/pkg/tui/tui_test.go TestDispatchWithoutCommander