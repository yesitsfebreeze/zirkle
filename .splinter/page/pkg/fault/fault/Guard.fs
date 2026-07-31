// §head page/pkg/fault/fault.go:34-43 Guard
// §sig func Guard(sink Sink, podID, where string)
	r := recover()
	if r == nil {
		return
	}
	stack := string(debug.Stack())
	msg := fmt.Sprint(r)
	stderr.Printf("PANIC in %s: %s\n%s", where, msg, stack)
	persist(sink, podID, KindPanic, where, msg, stack)
// §foot page/pkg/fault/fault.go Guard