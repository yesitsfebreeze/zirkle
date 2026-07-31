// §head page/pkg/egress/dial.go:76-88 Listen
// §sig func Listen(path string) (net.Listener, error)
	if err := os.MkdirAll(filepath.Dir(path), 0o700); err != nil {
		return nil, fmt.Errorf("egress: socket dir: %w", err)
	}
	if err := os.Remove(path); err != nil && !os.IsNotExist(err) {
		return nil, fmt.Errorf("egress: stale socket: %w", err)
	}
	l, err := net.Listen("unix", path)
	if err != nil {
		return nil, fmt.Errorf("egress: listen %s: %w", path, err)
	}
	return l, nil
// §foot page/pkg/egress/dial.go Listen