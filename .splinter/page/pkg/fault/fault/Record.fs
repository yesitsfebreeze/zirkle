// §head page/pkg/fault/fault.go:55-61 Record
// §sig func Record(sink Sink, podID, where string, err error)
	if err == nil {
		return
	}
	stderr.Printf("ERROR in %s: %v", where, err)
	persist(sink, podID, KindError, where, err.Error(), "")
// §foot page/pkg/fault/fault.go Record