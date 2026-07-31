// §source page/pkg/egress/socks5_test.go
package egress

import (
	"fmt"
	"io"
	"net"
	"testing"
	"time"
)

func startSOCKS5Proxy(t *testing.T, p *Policy) (*SOCKS5Proxy, string) {
// §.splinter/page/pkg/egress/socks5_test/startSOCKS5Proxy.fs
}

// socks5Dial performs a full SOCKS5 handshake over conn and returns the
// reply code.  On success (reply 0) the caller can read/write directly on
// conn.  On failure conn is closed.
func socks5Dial(t *testing.T, conn net.Conn, host string, port uint16) byte {
// §.splinter/page/pkg/egress/socks5_test/socks5Dial.fs
}

func TestSOCKS5ConnectAllowed(t *testing.T) {
// §.splinter/page/pkg/egress/socks5_test/TestSOCKS5ConnectAllowed.fs
}

func TestSOCKS5ConnectDenied(t *testing.T) {
// §.splinter/page/pkg/egress/socks5_test/TestSOCKS5ConnectDenied.fs
}

func TestSOCKS5ConnectDeniedNeverConnects(t *testing.T) {
// §.splinter/page/pkg/egress/socks5_test/TestSOCKS5ConnectDeniedNeverConnects.fs
}

func TestSOCKS5IPv4Literal(t *testing.T) {
// §.splinter/page/pkg/egress/socks5_test/TestSOCKS5IPv4Literal.fs
}

func TestSOCKS5UnsupportedCommand(t *testing.T) {
// §.splinter/page/pkg/egress/socks5_test/TestSOCKS5UnsupportedCommand.fs
}

func TestSOCKS5NoCommonAuth(t *testing.T) {
// §.splinter/page/pkg/egress/socks5_test/TestSOCKS5NoCommonAuth.fs
}

// TestSOCKS5TruncatedHandshake verifies that a client connecting and sending
// nothing (or a partial frame) closes without panicking the proxy.
func TestSOCKS5TruncatedHandshake(t *testing.T) {
// §.splinter/page/pkg/egress/socks5_test/TestSOCKS5TruncatedHandshake.fs
}

// TestSOCKS5AddressTypeIsBounded verifies that the domain length byte is
// bounded — a value of 255 must not cause an unbounded allocation.
func TestSOCKS5AddressTypeIsBounded(t *testing.T) {
// §.splinter/page/pkg/egress/socks5_test/TestSOCKS5AddressTypeIsBounded.fs
}

func FuzzSocks5Handshake(f *testing.F) {
// §.splinter/page/pkg/egress/socks5_test/FuzzSocks5Handshake.fs
}
