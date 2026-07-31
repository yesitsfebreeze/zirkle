// §head page/pkg/comp/dispatch_test.go:63-68 TestRenderNoVars
// §sig func TestRenderNoVars(t *testing.T)
	out := Render("hello world", nil)
	if out != "hello world" {
		t.Errorf("Render = %q", out)
	}
// §foot page/pkg/comp/dispatch_test.go TestRenderNoVars