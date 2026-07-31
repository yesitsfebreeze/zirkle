// §head page/pkg/comp/dispatch_test.go:24-35 TestPlatformStripNonMatching
// §sig func TestPlatformStripNonMatching(t *testing.T)
	tags := hostTags()
	other := "macos"
	if tags["macos"] {
		other = "windows"
	}
	jf := "[" + other + "]\ncheck:\n    echo " + other + "\n"
	stripped := PlatformStrip(jf)
	if strings.Contains(stripped, "echo "+other) {
		t.Errorf("non-matching recipe should be stripped: %q", stripped)
	}
// §foot page/pkg/comp/dispatch_test.go TestPlatformStripNonMatching