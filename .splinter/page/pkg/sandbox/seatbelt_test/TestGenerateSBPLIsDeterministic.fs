// §head page/pkg/sandbox/seatbelt_test.go:107-114 TestGenerateSBPLIsDeterministic
// §sig func TestGenerateSBPLIsDeterministic(t *testing.T)
	s := Spec{Dir: "/work", RW: []string{"/cache"}}
	a := GenerateSBPL(s)
	b := GenerateSBPL(s)
	if a != b {
		t.Fatal("GenerateSBPL is not deterministic for identical input")
	}
// §foot page/pkg/sandbox/seatbelt_test.go TestGenerateSBPLIsDeterministic