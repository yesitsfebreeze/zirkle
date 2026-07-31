// §head page/pkg/hotreload/hotreload_test.go:92-117 TestDevFlags
// §sig func TestDevFlags(t *testing.T)
	os.Unsetenv("RELAY_DEV_CHILD")
	os.Unsetenv("RELAY_DEV")

	if IsDevChild() {
		t.Errorf("IsDevChild() should be false")
	}
	if IsDevMode() {
		t.Errorf("IsDevMode() should be false")
	}

	os.Setenv("RELAY_DEV_CHILD", "1")
	if !IsDevChild() {
		t.Errorf("IsDevChild() should be true")
	}
	if !IsDevMode() {
		t.Errorf("IsDevMode() should be true")
	}
	os.Unsetenv("RELAY_DEV_CHILD")

	os.Setenv("RELAY_DEV", "1")
	if !IsDevMode() {
		t.Errorf("IsDevMode() should be true")
	}
	os.Unsetenv("RELAY_DEV")
// §foot page/pkg/hotreload/hotreload_test.go TestDevFlags