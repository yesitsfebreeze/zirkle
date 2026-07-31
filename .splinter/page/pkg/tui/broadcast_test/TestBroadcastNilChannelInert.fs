// §head page/pkg/tui/broadcast_test.go:45-54 TestBroadcastNilChannelInert
// §sig func TestBroadcastNilChannelInert(t *testing.T)
	m := New(mockSource{views: testViews()}, nil, nil)
	m2, _ := m.Update(broadcastTickMsg{})
	if m2.(Model).bc != nil {
		t.Fatal("nil channel should not activate a marquee")
	}
	if d := m2.(Model).renderDivider(); d != "" {
		t.Fatalf("renderDivider should be empty without a broadcast, got %q", d)
	}
// §foot page/pkg/tui/broadcast_test.go TestBroadcastNilChannelInert