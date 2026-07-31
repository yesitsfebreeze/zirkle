// §head page/pkg/comp/dispatch_test.go:70-76 TestFirstRecipeName
// §sig func TestFirstRecipeName(t *testing.T)
	jf := "# comment\ncheck-ci:\n    gh run list\n"
	name := firstRecipeName(jf)
	if name != "check-ci" {
		t.Errorf("expected check-ci, got %q", name)
	}
// §foot page/pkg/comp/dispatch_test.go TestFirstRecipeName