// §head page/pkg/webhook/webhook.go:206-211 Webhook.isDuplicate
// §sig func (w *Webhook) isDuplicate(ikey string) bool
	w.mu.RLock()
	defer w.mu.RUnlock()
	_, ok := w.seen[ikey]
	return ok
// §foot page/pkg/webhook/webhook.go Webhook.isDuplicate