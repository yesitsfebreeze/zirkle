// §head page/pkg/comp/dispatch_test.go:78-83 TestFirstRecipeNameNone
// §sig func TestFirstRecipeNameNone(t *testing.T)
	name := firstRecipeName("# just a comment\n")
	if name != "" {
		t.Errorf("expected empty, got %q", name)
	}
// §foot page/pkg/comp/dispatch_test.go TestFirstRecipeNameNone