// §head page/pkg/webhook/webhook.go:49-59 New
// §sig func New(secret string, port int) *Webhook
	if port <= 0 {
		port = defaultPort
	}
	return &Webhook{
		secret:  secret,
		port:    port,
		timeout: handlerTO,
		seen:    make(map[string]time.Time),
	}
// §foot page/pkg/webhook/webhook.go New