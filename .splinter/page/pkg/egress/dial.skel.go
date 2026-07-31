// §source page/pkg/egress/dial.go
package egress

import (
	"context"
	"errors"
	"fmt"
	"io"
	"net"
	"os"
	"path/filepath"
	"time"
)

// ErrDenied is what every refusal returns, whichever proxy asked.  The two
// proxies speak different wire protocols and have to answer in their own
// vocabulary — an HTTP status, a SOCKS5 reply byte — so they need one sentinel
// to recognise rather than one error string to parse.
var ErrDenied = errors.New("egress: host denied by policy")

// dialTimeout bounds a connection to an allowed host.  A sandboxed process
// cannot reach the network any other way, so a proxy goroutine wedged on a
// dead host is a leak the process inside cannot route around.
const dialTimeout = 30 * time.Second

// Dial checks addr against the policy and connects to it.  Every path out of
// the sandbox goes through here, so the check cannot be forgotten by a proxy
// that only meant to open a socket.
//
// addr is "host:port" as the client supplied it; the port is not part of the
// decision (see the host-scoped limit in docs/specs/f13-egress.md), it is only
// where the connection lands.
func (p *Policy) Dial(ctx context.Context, addr string) (net.Conn, error) {
// §.splinter/page/pkg/egress/dial/Policy.Dial.fs
}

// Relay copies between two connections until one direction ends, then closes
// both and waits for the other to unwind.  This is the unix↔tcp bridge
// Anthropic's sandbox-runtime needs `socat` for: Go does it with net.Conn and
// io.Copy, so the sandbox costs no external binary.
//
// The first direction to finish ends the pair. Waiting for both instead would
// hold every connection open until the idle side timed out, and a conn with no
// half-close — net.Pipe, a TLS record layer — has no idle side that ever ends.
func Relay(a, b net.Conn) {
// §.splinter/page/pkg/egress/dial/Relay.fs
}

// copyOneWay half-closes the destination when the source is spent, so a
// protocol that waits for EOF before answering — plenty of them do — gets that
// EOF while the other direction is still live.
func copyOneWay(dst, src net.Conn) {
// §.splinter/page/pkg/egress/dial/copyOneWay.fs
}

// Listen opens a unix socket at path, clearing a socket left behind by a
// process that died before it could unlink its own.  The sandbox binds this
// path in, so a stale file means no network at all for the next pod.
func Listen(path string) (net.Listener, error) {
// §.splinter/page/pkg/egress/dial/Listen.fs
}
