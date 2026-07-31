// §head page/pkg/tui/tui_test.go:57-63 TestEmptyListView
// §sig func TestEmptyListView(t *testing.T)
	got, _ := New(mockSource{}, nil, nil).Update(refreshMsg(nil))
	mm := got.(Model)
	if mm.View() == "" {
		t.Fatal("want non-empty view")
	}
// §foot page/pkg/tui/tui_test.go TestEmptyListView