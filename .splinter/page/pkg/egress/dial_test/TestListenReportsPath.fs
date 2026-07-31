// §head page/pkg/egress/dial_test.go:161-169 TestListenReportsPath
// §sig func TestListenReportsPath(t *testing.T)
	_, err := Listen(filepath.Join(t.TempDir(), "a\x00b", "s.sock"))
	if err == nil {
		t.Fatal("Listen on an unusable path = nil error")
	}
	if !strings.HasPrefix(err.Error(), "egress: ") {
		t.Errorf("error %q is not attributed to egress", err)
	}
// §foot page/pkg/egress/dial_test.go TestListenReportsPath