// §head page/pkg/tui/tui_test.go:43-55 TestCollapse
// §sig func TestCollapse(t *testing.T)
	m := New(mockSource{views: testViews()}, nil, nil)
	got, _ := m.Update(refreshMsg(testViews()))
	mm := got.(Model)
	if len(mm.visible()) != 3 {
		t.Fatalf("want 3 visible before collapse, got %d", len(mm.visible()))
	}
	// reverseGroups puts beta first, alpha (with child) second — collapse index 1.
	mm.collapsed[1] = true
	if len(mm.visible()) != 2 {
		t.Fatalf("want 2 visible after collapse, got %d", len(mm.visible()))
	}
// §foot page/pkg/tui/tui_test.go TestCollapse