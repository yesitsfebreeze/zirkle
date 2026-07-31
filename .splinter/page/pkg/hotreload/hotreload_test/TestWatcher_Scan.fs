// §head page/pkg/hotreload/hotreload_test.go:11-57 TestWatcher_Scan
// §sig func TestWatcher_Scan(t *testing.T)
	tmpDir, err := os.MkdirTemp("", "hotreload-test-*")
	if err != nil {
		t.Fatal(err)
	}
	defer os.RemoveAll(tmpDir)

	file1 := filepath.Join(tmpDir, "main.go")
	if err := os.WriteFile(file1, []byte("package main"), 0o644); err != nil {
		t.Fatal(err)
	}

	cfg := Config{
		RootDir:      tmpDir,
		PollInterval: 50 * time.Millisecond,
	}
	w := NewWatcher(cfg)

	// First scan populates initial state (no change detected on initial scan)
	if changed := w.Scan(); changed {
		t.Errorf("expected false on initial scan, got true")
	}

	// Modify file1
	time.Sleep(10 * time.Millisecond)
	if err := os.WriteFile(file1, []byte("package main\n// update"), 0o644); err != nil {
		t.Fatal(err)
	}

	if changed := w.Scan(); !changed {
		t.Errorf("expected true after file update, got false")
	}

	// Scan again without changes
	if changed := w.Scan(); changed {
		t.Errorf("expected false on second scan without modifications, got true")
	}

	// Delete file
	if err := os.Remove(file1); err != nil {
		t.Fatal(err)
	}

	if changed := w.Scan(); !changed {
		t.Errorf("expected true after file deletion, got false")
	}
// §foot page/pkg/hotreload/hotreload_test.go TestWatcher_Scan