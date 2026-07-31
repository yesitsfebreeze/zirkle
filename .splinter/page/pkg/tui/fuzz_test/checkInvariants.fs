// §head page/pkg/tui/fuzz_test.go:42-80 checkInvariants
// §sig func checkInvariants(t *testing.T, m Model)
	t.Helper()

	vis := m.visible()
	if m.cursor < 0 {
		t.Fatalf("cursor went negative: %d", m.cursor)
	}
	if len(vis) > 0 && m.cursor >= len(vis) {
		t.Fatalf("cursor %d past %d visible rows", m.cursor, len(vis))
	}
	// selectedIdx must be -1 or a real index into views. Returning a
	// valid-looking 0 for an empty list is exactly what crashed the daemon.
	if idx := m.selectedIdx(); idx != -1 && (idx < 0 || idx >= len(m.views)) {
		t.Fatalf("selectedIdx = %d, outside views (len %d)", idx, len(m.views))
	}
	if len(m.views) == 0 && m.selectedIdx() != -1 {
		t.Fatalf("selectedIdx = %d on empty views, want -1", m.selectedIdx())
	}

	// Mode is one state: the flags derived from it and the buffer that feeds
	// search must never drift apart. Deriving the mode from the buffer text is
	// what let them disagree and broke the input.
	if m.search != (m.mode == modeSearch) {
		t.Fatalf("search = %v with mode %v", m.search, m.mode)
	}
	if m.help != (m.mode == modeHelp) {
		t.Fatalf("help = %v with mode %v", m.help, m.mode)
	}
	if m.mode == modeSearch && m.searchQ != m.input.Value() {
		t.Fatalf("searchQ = %q but buffer = %q", m.searchQ, m.input.Value())
	}
	if m.mode != modeSearch && m.searchQ != "" {
		t.Fatalf("searchQ = %q outside search mode", m.searchQ)
	}

	// Rendering must survive whatever state the keys produced.
	_ = m.View()
	_ = m.treeContent()
// §foot page/pkg/tui/fuzz_test.go checkInvariants