// §head page/pkg/comp/dispatch_test.go:37-43 TestPlatformStripNoTags
// §sig func TestPlatformStripNoTags(t *testing.T)
	jf := "check:\n    echo hi\n"
	stripped := PlatformStrip(jf)
	if stripped != jf {
		t.Errorf("untagged justfile should be unchanged: got %q", stripped)
	}
// §foot page/pkg/comp/dispatch_test.go TestPlatformStripNoTags