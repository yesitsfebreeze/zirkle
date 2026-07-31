// §head page/pkg/comp/dispatch_test.go:45-54 TestPlatformStripNonPlatformAttr
// §sig func TestPlatformStripNonPlatformAttr(t *testing.T)
	jf := "[no-cd]\ncheck:\n    echo hi\n"
	stripped := PlatformStrip(jf)
	if !strings.Contains(stripped, "[no-cd]") {
		t.Errorf("non-platform attribute should be kept: %q", stripped)
	}
	if !strings.Contains(stripped, "echo hi") {
		t.Errorf("recipe should be kept: %q", stripped)
	}
// §foot page/pkg/comp/dispatch_test.go TestPlatformStripNonPlatformAttr