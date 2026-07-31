// §head page/pkg/webhook/webhook_test.go:229-233 panicSink.len
// §sig func (p *panicSink) len() int
	p.mu.Lock()
	defer p.mu.Unlock()
	return len(p.rows)
// §foot page/pkg/webhook/webhook_test.go panicSink.len