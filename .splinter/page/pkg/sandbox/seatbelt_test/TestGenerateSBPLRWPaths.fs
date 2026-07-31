// §head page/pkg/sandbox/seatbelt_test.go:82-94 TestGenerateSBPLRWPaths
// §sig func TestGenerateSBPLRWPaths(t *testing.T)
	s := Spec{
		Dir: "/work",
		RW:  []string{"/cache", "/tmp/shared"},
	}
	got := GenerateSBPL(s)
	if !strings.Contains(got, "(allow file-write* (subpath \"/cache\"))") {
		t.Fatalf("missing RW write-allow for /cache:\n%s", got)
	}
	if !strings.Contains(got, "(allow file-write* (subpath \"/tmp/shared\"))") {
		t.Fatalf("missing RW write-allow for /tmp/shared:\n%s", got)
	}
// §foot page/pkg/sandbox/seatbelt_test.go TestGenerateSBPLRWPaths