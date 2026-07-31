// §head page/pkg/tui/broadcast_test.go:5-43 TestBroadcastMarquee
// §sig func TestBroadcastMarquee(t *testing.T)
	bc := make(chan string, 1)
	m := New(mockSource{views: testViews()}, nil, bc)

	if m.bc != nil {
		t.Fatal("expected nil marquee at startup")
	}
	if d := m.renderDivider(); d != "" {
		t.Fatalf("renderDivider should be empty without a broadcast, got %q", d)
	}

	bc <- "URGENT: deploy now"
	m2, _ := m.Update(broadcastTickMsg{})
	mm := m2.(Model)
	if mm.bc == nil {
		t.Fatal("expected marquee active after broadcast tick")
	}
	if mm.bc.text != "URGENT: deploy now" {
		t.Fatalf("marquee text = %q", mm.bc.text)
	}
	if mm.bc.pos <= 0 {
		t.Fatalf("marquee should start off-screen right, got pos %d", mm.bc.pos)
	}
	startPos := mm.bc.pos

	m3, _ := mm.Update(scrollTickMsg{})
	mm = m3.(Model)
	if mm.bc == nil || mm.bc.pos != startPos-1 {
		t.Fatalf("scroll tick should decrement pos by 1, got %v", mm.bc)
	}

	for mm.bc != nil {
		m4, _ := mm.Update(scrollTickMsg{})
		mm = m4.(Model)
	}
	if mm.bc != nil {
		t.Fatal("marquee should clear after scrolling past")
	}
// §foot page/pkg/tui/broadcast_test.go TestBroadcastMarquee