// §head page/pkg/egress/http.go:38-44 HTTPProxy.serveHTTP
// §sig func (px *HTTPProxy) serveHTTP(w http.ResponseWriter, r *http.Request)
	if r.Method == http.MethodConnect {
		px.serveConnect(w, r)
		return
	}
	px.servePlain(w, r)
// §foot page/pkg/egress/http.go HTTPProxy.serveHTTP