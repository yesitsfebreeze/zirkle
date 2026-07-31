// §head page/pkg/tui/tui_test.go:34-41 TestRefreshPopulatesViews
// §sig func TestRefreshPopulatesViews(t *testing.T)
	m := New(mockSource{views: testViews()}, nil, nil)
	got, _ := m.Update(refreshMsg(testViews()))
	mm := got.(Model)
	if len(mm.Views()) != 3 {
		t.Fatalf("want 3 views, got %d", len(mm.Views()))
	}
// §foot page/pkg/tui/tui_test.go TestRefreshPopulatesViews