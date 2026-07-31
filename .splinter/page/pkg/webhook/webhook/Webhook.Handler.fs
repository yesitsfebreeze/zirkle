// §head page/pkg/webhook/webhook.go:63-67 Webhook.Handler
// §sig func (w *Webhook) Handler() http.Handler
	mux := http.NewServeMux()
	mux.HandleFunc("/hook/", w.handleHook)
	return mux
// §foot page/pkg/webhook/webhook.go Webhook.Handler