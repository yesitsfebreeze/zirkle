// §head page/pkg/egress/http.go:90-125 HTTPProxy.serveConnect
// §sig func (px *HTTPProxy) serveConnect(w http.ResponseWriter, r *http.Request)
	if !px.policy.Allow(hostOnly(r.Host)) {
		http.Error(w, "Forbidden", http.StatusForbidden)
		return
	}
	hj, ok := w.(http.Hijacker)
	if !ok {
		http.Error(w, "Not Hijackable", http.StatusInternalServerError)
		return
	}
	client, _, err := hj.Hijack()
	if err != nil {
		http.Error(w, err.Error(), http.StatusInternalServerError)
		return
	}
	fmt.Fprintf(client, "HTTP/1.1 200 Connection Established\r\n\r\n")
	peeked, sni, err := peekSNI(client)
	if err != nil && !errors.Is(err, ErrNotTLS) && !errors.Is(err, ErrNoSNI) {
		client.Close()
		return
	}
	if sni != "" && !px.policy.Allow(sni) {
		client.Close()
		return
	}
	upstream, err := px.policy.Dial(context.Background(), r.Host)
	if err != nil {
		client.Close()
		return
	}
	defer upstream.Close()
	if len(peeked) > 0 {
		upstream.Write(peeked)
	}
	Relay(client, upstream)
// §foot page/pkg/egress/http.go HTTPProxy.serveConnect