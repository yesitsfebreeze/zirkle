// §head page/pkg/sandbox/seatbelt_test.go:63-80 TestGenerateSBPLCustomTools
// §sig func TestGenerateSBPLCustomTools(t *testing.T)
	s := Spec{
		Dir:   "/work",
		Tools: []string{"/usr/local/bin", "/opt/go"},
	}
	got := GenerateSBPL(s)
	// Custom tools replace defaults.
	if !strings.Contains(got, "(allow file-read* (subpath \"/usr/local/bin\"))") {
		t.Fatalf("missing custom tool read-allow:\n%s", got)
	}
	if !strings.Contains(got, "(allow process-exec (subpath \"/opt/go\"))") {
		t.Fatalf("missing custom tool exec-allow:\n%s", got)
	}
	// Default tools must NOT appear when custom Tools is set.
	if strings.Contains(got, "(allow file-read* (subpath \"/etc/passwd\"))") {
		t.Fatalf("default tool leaked through when custom Tools set:\n%s", got)
	}
// §foot page/pkg/sandbox/seatbelt_test.go TestGenerateSBPLCustomTools