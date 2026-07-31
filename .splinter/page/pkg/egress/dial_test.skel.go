// §source page/pkg/egress/dial_test.go
package egress

import (
	"context"
	"errors"
	"io"
	"net"
	"path/filepath"
	"strings"
	"testing"
)

// echoServer answers with whatever it is sent, so a relay can be checked by
// round-tripping a byte through it.
func echoServer(t *testing.T) net.Listener {
// §.splinter/page/pkg/egress/dial_test/echoServer.fs
}

func TestDialAllowed(t *testing.T) {
// §.splinter/page/pkg/egress/dial_test/TestDialAllowed.fs
}

func TestDialDenied(t *testing.T) {
// §.splinter/page/pkg/egress/dial_test/TestDialDenied.fs
}

// A denied host must be refused before a socket is opened, not after — the
// point of the boundary is that the connection never happens.
func TestDialDeniedNeverConnects(t *testing.T) {
// §.splinter/page/pkg/egress/dial_test/TestDialDeniedNeverConnects.fs
}

func TestDialBadAddress(t *testing.T) {
// §.splinter/page/pkg/egress/dial_test/TestDialBadAddress.fs
}

func TestRelay(t *testing.T) {
// §.splinter/page/pkg/egress/dial_test/TestRelay.fs
}

// Relay must close both sides once either direction ends, or a proxy leaks a
// goroutine and a file descriptor per connection.
func TestRelayClosesBothSides(t *testing.T) {
// §.splinter/page/pkg/egress/dial_test/TestRelayClosesBothSides.fs
}

func TestListen(t *testing.T) {
// §.splinter/page/pkg/egress/dial_test/TestListen.fs
}

func TestListenReportsPath(t *testing.T) {
// §.splinter/page/pkg/egress/dial_test/TestListenReportsPath.fs
}
