// §source page/pkg/egress/http_test.go
package egress

import (
	"bufio"
	"bytes"
	"fmt"
	"io"
	"net"
	"net/http"
	"net/http/httptest"
	"path/filepath"
	"strings"
	"testing"
	"time"
)

// socketPath returns a writable unix socket path inside t's temp dir.
func socketPath(t testing.TB, name string) string {
// §.splinter/page/pkg/egress/http_test/socketPath.fs
}

// startHTTPProxy starts a new HTTP proxy on a random unix socket and returns
// the proxy and the socket path.  The caller dials the path, the proxy
// dials upstream through the policy.
func startHTTPProxy(t *testing.T, p *Policy) (*HTTPProxy, string) {
// §.splinter/page/pkg/egress/http_test/startHTTPProxy.fs
}

func TestHTTPProxyPlain(t *testing.T) {
// §.splinter/page/pkg/egress/http_test/TestHTTPProxyPlain.fs
}

func TestHTTPProxyPlainDenied(t *testing.T) {
// §.splinter/page/pkg/egress/http_test/TestHTTPProxyPlainDenied.fs
}

func TestHTTPProxyCONNECT(t *testing.T) {
// §.splinter/page/pkg/egress/http_test/TestHTTPProxyCONNECT.fs
}

// A denied CONNECT must be refused before any upstream bytes flow.  We
// assert the upstream listener never accepted a connection at all.
func TestHTTPProxyCONNECTDeniedNeverConnects(t *testing.T) {
// §.splinter/page/pkg/egress/http_test/TestHTTPProxyCONNECTDeniedNeverConnects.fs
}

func TestHTTPProxyCONNECTDenied(t *testing.T) {
// §.splinter/page/pkg/egress/http_test/TestHTTPProxyCONNECTDenied.fs
}

// TestHTTPProxyStripsHopByHop asserts the proxy does not forward headers
// an intermediary must not forward.
func TestHTTPProxyStripsHopByHop(t *testing.T) {
// §.splinter/page/pkg/egress/http_test/TestHTTPProxyStripsHopByHop.fs
}

// newEchoListener returns a TCP listener that echoes every byte it receives
// until the connection closes.  Uses Read/Write instead of io.Copy(c, c) to
// avoid splice deadlocking when source == destination on Linux.
func newEchoListener(t *testing.T) net.Listener {
// §.splinter/page/pkg/egress/http_test/newEchoListener.fs
}
