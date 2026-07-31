// §head page/pkg/webhook/webhook.go:214-232 Webhook.cleanupLoop
// §sig func (w *Webhook) cleanupLoop(ctx context.Context)
	ticker := time.NewTicker(cleanupInt)
	defer ticker.Stop()
	for {
		select {
		case <-ctx.Done():
			return
		case <-ticker.C:
			w.mu.Lock()
			now := time.Now()
			for k, v := range w.seen {
				if now.Sub(v) > dedupTTL {
					delete(w.seen, k)
				}
			}
			w.mu.Unlock()
		}
	}
// §foot page/pkg/webhook/webhook.go Webhook.cleanupLoop