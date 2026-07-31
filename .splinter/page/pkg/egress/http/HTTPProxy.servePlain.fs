// §head page/pkg/egress/http.go:46-88 HTTPProxy.servePlain
// §sig func (px *HTTPProxy) servePlain(w http.ResponseWriter, r *http.Request)
	host := r.Host
	if host == "" {
		host = r.URL.Host
	}
	if !px.policy.Allow(hostOnly(host)) {
		http.Error(w, "Forbidden", http.StatusForbidden)
		return
	}
	upstream := r.URL
	upstream.Scheme = "http"
	if upstream.Host == "" {
		upstream.Host = host
	}
	req := r.Clone(context.Background())
	stripHopByHop(req.Header)
	req.RequestURI = ""
	req.URL = upstream
	conn, err := px.policy.Dial(context.Background(), host)
	if err != nil {
		http.Error(w, "Forbidden", http.StatusForbidden)
		return
	}
	defer conn.Close()
	if err := req.Write(conn); err != nil {
		http.Error(w, "Bad Gateway", http.StatusBadGateway)
		return
	}
	resp, err := http.ReadResponse(bufio.NewReader(conn), req)
	if err != nil {
		http.Error(w, "Bad Gateway", http.StatusBadGateway)
		return
	}
	defer resp.Body.Close()
	stripHopByHop(resp.Header)
	for k, vs := range resp.Header {
		for _, v := range vs {
			w.Header().Add(k, v)
		}
	}
	w.WriteHeader(resp.StatusCode)
	io.Copy(w, resp.Body)
// §foot page/pkg/egress/http.go HTTPProxy.servePlain