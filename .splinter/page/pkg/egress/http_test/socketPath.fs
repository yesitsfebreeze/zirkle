// §head page/pkg/egress/http_test.go:18-21 socketPath
// §sig func socketPath(t testing.TB, name string) string
	t.Helper()
	return filepath.Join(t.TempDir(), name+".sock")
// §foot page/pkg/egress/http_test.go socketPath