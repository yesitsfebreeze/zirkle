// §head page/pkg/comp/dispatch_test.go:56-61 TestRender
// §sig func TestRender(t *testing.T)
	out := Render("hello <<name>> <<name>>", map[string]string{"name": "world"})
	if out != "hello world world" {
		t.Errorf("Render = %q", out)
	}
// §foot page/pkg/comp/dispatch_test.go TestRender