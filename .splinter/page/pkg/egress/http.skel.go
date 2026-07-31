// §source page/pkg/egress/http.go
package egress

import (
	"bufio"
	"context"
	"errors"
	"fmt"
	"io"
	"net"
	"net/http"
	"strings"
	"time"
)

type HTTPProxy struct {
	policy *Policy
	server *http.Server
}

func NewHTTPProxy(p *Policy) *HTTPProxy {
// §.splinter/page/pkg/egress/http/NewHTTPProxy.fs
}

func (px *HTTPProxy) Serve(l net.Listener) error {
// §.splinter/page/pkg/egress/http/HTTPProxy.Serve.fs
}

func (px *HTTPProxy) Close() error {
// §.splinter/page/pkg/egress/http/HTTPProxy.Close.fs
}

func (px *HTTPProxy) serveHTTP(w http.ResponseWriter, r *http.Request) {
// §.splinter/page/pkg/egress/http/HTTPProxy.serveHTTP.fs
}

func (px *HTTPProxy) servePlain(w http.ResponseWriter, r *http.Request) {
// §.splinter/page/pkg/egress/http/HTTPProxy.servePlain.fs
}

func (px *HTTPProxy) serveConnect(w http.ResponseWriter, r *http.Request) {
// §.splinter/page/pkg/egress/http/HTTPProxy.serveConnect.fs
}

func hostOnly(host string) string {
// §.splinter/page/pkg/egress/http/hostOnly.fs
}

func stripHopByHop(h http.Header) {
// §.splinter/page/pkg/egress/http/stripHopByHop.fs
}
