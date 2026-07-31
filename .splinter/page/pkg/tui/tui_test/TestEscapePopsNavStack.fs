// §head page/pkg/tui/tui_test.go:337-398 TestEscapePopsNavStack
// §sig func TestEscapePopsNavStack(t *testing.T)
	m := New(mockSource{views: testViews()}, &recordingCommander{}, nil)
	next, _ := m.Update(tea.WindowSizeMsg{Width: 80, Height: 24})
	mm := next.(Model)

	// Start in pods pane with detail open (deepest state).
	mm.pane = 0
	mm.input.Blur()
	mm.detail = "child1"

	// 1: detail → pods pane (still pane 0).
	next, _ = mm.Update(tea.KeyMsg{Type: tea.KeyEscape})
	mm = next.(Model)
	if mm.detail != "" {
		t.Fatalf("esc 1: detail=%q want empty", mm.detail)
	}
	if mm.pane != 0 {
		t.Fatalf("esc 1: pane=%d want 0 (pods)", mm.pane)
	}

	// 2: pods pane → input focused.
	next, _ = mm.Update(tea.KeyMsg{Type: tea.KeyEscape})
	mm = next.(Model)
	if mm.pane != 1 {
		t.Fatalf("esc 2: pane=%d want 1 (input)", mm.pane)
	}
	if !mm.input.Focused() {
		t.Error("esc 2: input not focused")
	}

	// 3: search mode → normal.
	mm.input.SetValue("query")
	mm.mode = modeSearch
	mm.search = true
	mm.searchQ = "query"
	next, _ = mm.Update(tea.KeyMsg{Type: tea.KeyEscape})
	mm = next.(Model)
	if mm.mode != modeNormal {
		t.Errorf("esc 3: mode=%v want modeNormal", mm.mode)
	}
	if mm.search {
		t.Error("esc 3: search still true")
	}
	if mm.input.Value() != "" {
		t.Errorf("esc 3: input=%q want empty", mm.input.Value())
	}

	// 4: input has text → cleared.
	mm.input.SetValue("hello")
	next, _ = mm.Update(tea.KeyMsg{Type: tea.KeyEscape})
	mm = next.(Model)
	if mm.input.Value() != "" {
		t.Errorf("esc 4: input=%q want empty", mm.input.Value())
	}

	// 5: home → no-op.
	next, _ = mm.Update(tea.KeyMsg{Type: tea.KeyEscape})
	mm = next.(Model)
	if mm.pane != 1 || mm.mode != modeNormal || mm.input.Value() != "" {
		t.Errorf("esc 5: not home: pane=%d mode=%v input=%q", mm.pane, mm.mode, mm.input.Value())
	}
// §foot page/pkg/tui/tui_test.go TestEscapePopsNavStack