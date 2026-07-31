// §head page/pkg/webhook/webhook_test.go:222-227 panicSink.RecordFault
// §sig func (p *panicSink) RecordFault(podID, kind, site, msg, stack string) error
	p.mu.Lock()
	defer p.mu.Unlock()
	p.rows = append(p.rows, kind+" "+site+" "+msg)
	return nil
// §foot page/pkg/webhook/webhook_test.go panicSink.RecordFault