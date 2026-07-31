// §head page/pkg/fault/fault_test.go:19-27 memSink.RecordFault
// §sig func (m *memSink) RecordFault(podID, kind, site, msg, stack string) error
	if m.boom {
		panic("sink exploded")
	}
	m.mu.Lock()
	defer m.mu.Unlock()
	m.rows = append(m.rows, row{podID, kind, site, msg, stack})
	return m.fail
// §foot page/pkg/fault/fault_test.go memSink.RecordFault