// §head page/pkg/egress/dial_test.go:142-159 TestListen
// §sig func TestListen(t *testing.T)
	path := filepath.Join(t.TempDir(), "nested", "egress.sock")

	l, err := Listen(path)
	if err != nil {
		t.Fatalf("Listen: %v", err)
	}
	l.Close()

	// A socket left by a process that died before unlinking must not block
	// the next one: the sandbox binds this path in, so a stale file means
	// no network at all for the next pod.
	l2, err := Listen(path)
	if err != nil {
		t.Fatalf("Listen over a stale socket: %v", err)
	}
	l2.Close()
// §foot page/pkg/egress/dial_test.go TestListen