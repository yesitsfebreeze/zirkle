// §head page/pkg/comp/dispatch_test.go:9-22 TestPlatformStripMatching
// §sig func TestPlatformStripMatching(t *testing.T)
	jf := "[unix]\ncheck:\n    echo unix\n\n[macos]\ncheck:\n    echo macos\n"
	stripped := PlatformStrip(jf)
	if strings.Contains(stripped, "[unix]") || strings.Contains(stripped, "[macos]") {
		t.Errorf("platform tags should be stripped: %q", stripped)
	}
	tags := hostTags()
	if tags["unix"] && !strings.Contains(stripped, "echo unix") {
		t.Errorf("unix recipe should remain on unix host: %q", stripped)
	}
	if tags["macos"] && !strings.Contains(stripped, "echo macos") {
		t.Errorf("macos recipe should remain on macos host: %q", stripped)
	}
// §foot page/pkg/comp/dispatch_test.go TestPlatformStripMatching