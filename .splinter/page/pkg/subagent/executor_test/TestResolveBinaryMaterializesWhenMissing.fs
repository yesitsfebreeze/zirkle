// §head page/pkg/subagent/executor_test.go:220-247 TestResolveBinaryMaterializesWhenMissing
// §sig func TestResolveBinaryMaterializesWhenMissing(t *testing.T)
	if _, err := os.Stat("/proc/self/exe"); err != nil {
		t.Skip("no /proc/self/exe on this host")
	}
	gone := filepath.Join(t.TempDir(), "deleted-relay")

	path, cleanup, err := resolveBinary(gone)
	if err != nil {
		t.Fatalf("resolveBinary: %v", err)
	}
	defer cleanup()

	fi, err := os.Stat(path)
	if err != nil {
		t.Fatalf("materialized binary missing: %v", err)
	}
	if fi.Mode()&0o100 == 0 {
		t.Fatalf("materialized binary not executable: %v", fi.Mode())
	}
	if fi.Size() == 0 {
		t.Fatal("materialized binary is empty")
	}

	cleanup()
	if _, err := os.Stat(path); !os.IsNotExist(err) {
		t.Fatalf("cleanup did not remove copy: %v", err)
	}
// §foot page/pkg/subagent/executor_test.go TestResolveBinaryMaterializesWhenMissing