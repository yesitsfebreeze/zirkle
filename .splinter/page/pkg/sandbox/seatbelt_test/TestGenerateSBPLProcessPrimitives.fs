// §head page/pkg/sandbox/seatbelt_test.go:96-105 TestGenerateSBPLProcessPrimitives
// §sig func TestGenerateSBPLProcessPrimitives(t *testing.T)
	s := Spec{Dir: "/x"}
	got := GenerateSBPL(s)
	if !strings.Contains(got, "(allow process-fork)") {
		t.Fatalf("missing process-fork:\n%s", got)
	}
	if !strings.Contains(got, "(allow signal (target self))") {
		t.Fatalf("missing self-signal:\n%s", got)
	}
// §foot page/pkg/sandbox/seatbelt_test.go TestGenerateSBPLProcessPrimitives