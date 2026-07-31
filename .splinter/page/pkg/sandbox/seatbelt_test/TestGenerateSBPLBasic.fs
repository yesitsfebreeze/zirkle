// §head page/pkg/sandbox/seatbelt_test.go:8-50 TestGenerateSBPLBasic
// §sig func TestGenerateSBPLBasic(t *testing.T)
	s := Spec{
		Dir: "/work/pod",
		Net: false,
	}
	got := GenerateSBPL(s)

	// Must deny by default.
	if !strings.Contains(got, "(deny default)") {
		t.Fatalf("missing (deny default):\n%s", got)
	}
	// Version header.
	if !strings.HasPrefix(got, "(version 1)") {
		t.Fatalf("missing version header:\n%s", got)
	}
	// Default tools must be read-allowed (they are the nil-Tools default).
	for _, tool := range DefaultTools {
		want := "(allow file-read* (subpath \"" + tool + "\"))"
		if !strings.Contains(got, want) {
			t.Fatalf("missing read-allow for default tool %q:\n%s", tool, got)
		}
	}
	// Default tools must be exec-allowed.
	if !strings.Contains(got, "(allow process-exec (subpath \"/usr\"))") {
		t.Fatalf("missing process-exec for /usr:\n%s", got)
	}
	// Dir must be write-allowed.
	if !strings.Contains(got, "(allow file-write* (subpath \"/work/pod\"))") {
		t.Fatalf("missing write-allow for Dir:\n%s", got)
	}
	// /tmp writable for process function.
	if !strings.Contains(got, "(allow file-write* (subpath \"/tmp\"))") {
		t.Fatalf("missing write-allow for /tmp:\n%s", got)
	}
	// Network denied by default.
	if !strings.Contains(got, "(deny network*)") {
		t.Fatalf("missing network deny:\n%s", got)
	}
	// Must NOT contain an allow-network when Net is false.
	if strings.Contains(got, "(allow network*)") {
		t.Fatalf("unexpected network allow:\n%s", got)
	}
// §foot page/pkg/sandbox/seatbelt_test.go TestGenerateSBPLBasic