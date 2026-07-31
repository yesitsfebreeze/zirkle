// §head page/pkg/fault/fault_test.go:29-33 memSink.len
// §sig func (m *memSink) len() int
	m.mu.Lock()
	defer m.mu.Unlock()
	return len(m.rows)
// §foot page/pkg/fault/fault_test.go memSink.len