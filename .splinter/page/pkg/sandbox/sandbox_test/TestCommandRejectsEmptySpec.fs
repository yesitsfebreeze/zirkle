// §head page/pkg/sandbox/sandbox_test.go:40-47 TestCommandRejectsEmptySpec
// §sig func TestCommandRejectsEmptySpec(t *testing.T)
	if _, err := (Spec{}).Command(context.Background(), "/bin/true"); err == nil {
		t.Fatal("expected error for Spec with no Dir")
	}
	if _, err := (Spec{Dir: t.TempDir()}).Command(context.Background()); err == nil {
		t.Fatal("expected error for empty argv")
	}
// §foot page/pkg/sandbox/sandbox_test.go TestCommandRejectsEmptySpec