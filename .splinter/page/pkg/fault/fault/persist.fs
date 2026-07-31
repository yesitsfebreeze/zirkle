// §head page/pkg/fault/fault.go:65-79 persist
// §sig func persist(sink Sink, podID, kind, where, msg, stack string)
	if sink == nil {
		return
	}
	mu.Lock()
	defer mu.Unlock()
	defer func() {
		if r := recover(); r != nil {
			stderr.Printf("fault sink panicked while recording %s in %s: %v", kind, where, r)
		}
	}()
	if err := sink.RecordFault(podID, kind, where, msg, stack); err != nil {
		stderr.Printf("fault sink failed while recording %s in %s: %v", kind, where, err)
	}
// §foot page/pkg/fault/fault.go persist