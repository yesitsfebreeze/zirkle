// §head page/pkg/tui/multinput_test.go:20-26 TestSingleLineDownEmitsBottom
// §sig func TestSingleLineDownEmitsBottom(t *testing.T)
	mi := NewMultiInput(60)
	_, _, evt := mi.Update(tea.KeyMsg{Type: tea.KeyDown})
	if evt != BoundaryBottom {
		t.Fatalf("down on single line = %v, want BoundaryBottom", evt)
	}
// §foot page/pkg/tui/multinput_test.go TestSingleLineDownEmitsBottom