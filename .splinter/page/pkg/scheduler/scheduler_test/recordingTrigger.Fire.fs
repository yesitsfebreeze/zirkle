// §head page/pkg/scheduler/scheduler_test.go:21-27 recordingTrigger.Fire
// §sig func (r *recordingTrigger) Fire(_ context.Context, agentID string) error
	atomic.AddInt32(&r.fired, 1)
	r.mu.Lock()
	r.last = agentID
	r.mu.Unlock()
	return r.err
// §foot page/pkg/scheduler/scheduler_test.go recordingTrigger.Fire