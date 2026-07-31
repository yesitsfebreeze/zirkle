// §head page/pkg/tui/multinput_test.go:11-17 TestSingleLineUpEmitsTop
// §sig func TestSingleLineUpEmitsTop(t *testing.T)
	mi := NewMultiInput(60)
	_, _, evt := mi.Update(tea.KeyMsg{Type: tea.KeyUp})
	if evt != BoundaryTop {
		t.Fatalf("up on single line = %v, want BoundaryTop", evt)
	}
// §foot page/pkg/tui/multinput_test.go TestSingleLineUpEmitsTop