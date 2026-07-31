// §head page/pkg/fault/fault.go:48-51 Recovered
// §sig func Recovered(sink Sink, podID, where, msg, stack string)
	stderr.Printf("PANIC in %s: %s\n%s", where, msg, stack)
	persist(sink, podID, KindPanic, where, msg, stack)
// §foot page/pkg/fault/fault.go Recovered