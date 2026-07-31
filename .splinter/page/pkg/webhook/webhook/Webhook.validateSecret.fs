// §head page/pkg/webhook/webhook.go:200-203 Webhook.validateSecret
// §sig func (w *Webhook) validateSecret(got string) bool
	// If secret were empty the server would never start, but be defensive.
	return w.secret != "" && subtle.ConstantTimeCompare([]byte(got), []byte(w.secret)) == 1
// §foot page/pkg/webhook/webhook.go Webhook.validateSecret