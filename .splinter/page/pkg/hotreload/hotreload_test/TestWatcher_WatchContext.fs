// §head page/pkg/hotreload/hotreload_test.go:59-90 TestWatcher_WatchContext
// §sig func TestWatcher_WatchContext(t *testing.T)
	tmpDir, err := os.MkdirTemp("", "hotreload-test-*")
	if err != nil {
		t.Fatal(err)
	}
	defer os.RemoveAll(tmpDir)

	cfg := Config{
		RootDir:      tmpDir,
		PollInterval: 20 * time.Millisecond,
	}
	w := NewWatcher(cfg)
	events := make(chan struct{}, 10)

	ctx, cancel := context.WithCancel(context.Background())
	go w.Watch(ctx, events)

	// Cancel context immediately
	cancel()
	time.Sleep(50 * time.Millisecond)

	// Add file after cancel
	file1 := filepath.Join(tmpDir, "test.go")
	_ = os.WriteFile(file1, []byte("package test"), 0o644)

	time.Sleep(50 * time.Millisecond)
	select {
	case <-events:
		t.Errorf("unexpected event received after context cancellation")
	default:
	}
// §foot page/pkg/hotreload/hotreload_test.go TestWatcher_WatchContext