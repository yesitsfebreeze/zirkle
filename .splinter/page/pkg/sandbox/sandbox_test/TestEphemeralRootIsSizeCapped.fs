// §head page/pkg/sandbox/sandbox_test.go:142-153 TestEphemeralRootIsSizeCapped
// §sig func TestEphemeralRootIsSizeCapped(t *testing.T)
	requireSandbox(t)

	out, ok := run(t, Spec{Dir: t.TempDir(), Ephemeral: true, SizeMB: 8},
		"dd if=/dev/zero of="+Root+"/fill bs=1M count=64 2>&1")
	if ok {
		t.Fatalf("wrote 64M into an 8M root: %s", out)
	}
	if !strings.Contains(out, "space") {
		t.Logf("size cap enforced, message: %s", strings.TrimSpace(out))
	}
// §foot page/pkg/sandbox/sandbox_test.go TestEphemeralRootIsSizeCapped