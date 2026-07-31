// §head page/pkg/sandbox/landlock_test.go:21-76 TestLandlockDeniesWrite
// §sig func TestLandlockDeniesWrite(t *testing.T)
	if LandlockABI() == 0 {
		t.Skip("Landlock unavailable")
	}

	// Landlock restricts the current thread only.  Fork a subprocess that
	// applies Landlock then tries to write a denied path, expecting EACCES.
	if os.Getenv("RELAY_LANDLOCK_TEST") != "1" {
		cmd := exec.Command(os.Args[0], "-test.run=TestLandlockDeniesWrite")
		cmd.Env = append(os.Environ(), "RELAY_LANDLOCK_TEST=1")
		out, err := cmd.CombinedOutput()
		t.Logf("subprocess output: %s", out)
		if err != nil {
			t.Fatalf("subprocess failed: %v (see output above)", err)
		}
		return
	}

	// Inside the subprocess: apply Landlock allowing read on /, write
	// only on /tmp.  Then try to write to a path outside /tmp and expect
	// EACCES.  The target must NOT be under /tmp (t.TempDir uses /tmp).
	// ponytail: per-thread only; subprocess isolation is the only way to
	// test Landlock in Go without polluting the test runner's threads.
	cwd, err := os.Getwd()
	if err != nil {
		t.Fatal(err)
	}
	dir := filepath.Join(cwd, ".landlock-test")
	os.MkdirAll(dir, 0o755)
	defer os.RemoveAll(dir)
	target := filepath.Join(dir, "blocked")
	if err := os.WriteFile(target, []byte("x"), 0644); err != nil {
		t.Fatal(err)
	}

	runtime.LockOSThread()
	defer runtime.UnlockOSThread()

	if err := ApplyLandlock(
		[]string{"/", "/tmp", "/proc", "/dev"},
		[]string{"/tmp"},
	); err != nil {
		t.Fatalf("ApplyLandlock: %v", err)
	}

	// Try to write to target — should fail with EACCES (cwd is not /tmp).
	f, err := os.OpenFile(target, os.O_WRONLY|os.O_TRUNC, 0)
	if err == nil {
		f.Close()
		t.Fatal("write to denied path succeeded — Landlock not enforcing")
	}
	if pe, ok := err.(*os.PathError); ok && pe.Err == unix.EACCES {
		return // Expected.
	}
	t.Fatalf("expected EACCES, got: %v", err)
// §foot page/pkg/sandbox/landlock_test.go TestLandlockDeniesWrite