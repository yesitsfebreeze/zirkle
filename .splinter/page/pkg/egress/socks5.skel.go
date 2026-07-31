// §source page/pkg/egress/socks5.go
package egress

import (
	"context"
	"encoding/binary"
	"errors"
	"fmt"
	"io"
	"net"
	"time"
)

const (
	socksVer5       = 5
	socksAuthNone   = 0
	socksCmdConnect = 1
	socksCmdBind    = 2
	socksCmdAssoc   = 3

	socksAtypIPv4       = 1
	socksAtypDomainName = 3
	socksAtypIPv6       = 4

	socksRepSuccess              = 0
	socksRepGeneralFailure       = 1
	socksRepConnectionNotAllowed = 2
	socksRepNetworkUnreachable   = 3
	socksRepHostUnreachable      = 4
	socksRepConnectionRefused    = 5
	socksRepTTLExpired           = 6
	socksRepCommandNotSupported  = 7
	socksRepAddressTypeNotSup    = 8
)

var (
	errUnsupportedVersion  = errors.New("egress: socks5: unsupported version")
	errNoCommonAuth        = errors.New("egress: socks5: no common auth method")
	errUnsupportedCommand  = errors.New("egress: socks5: unsupported command")
	errUnsupportedAddrType = errors.New("egress: socks5: unsupported address type")
	errShortRead           = errors.New("egress: socks5: short read")
)

const handshakeDeadline = 10 * time.Second

type SOCKS5Proxy struct {
	policy *Policy
}

func NewSOCKS5Proxy(p *Policy) *SOCKS5Proxy {
// §.splinter/page/pkg/egress/socks5/NewSOCKS5Proxy.fs
}

func (px *SOCKS5Proxy) Serve(l net.Listener) error {
// §.splinter/page/pkg/egress/socks5/SOCKS5Proxy.Serve.fs
}

func (px *SOCKS5Proxy) handle(conn net.Conn) {
// §.splinter/page/pkg/egress/socks5/SOCKS5Proxy.handle.fs
}

func (px *SOCKS5Proxy) authNegotiate(rw io.ReadWriter) bool {
// §.splinter/page/pkg/egress/socks5/SOCKS5Proxy.authNegotiate.fs
}

func (px *SOCKS5Proxy) command(r io.Reader) (string, byte) {
// §.splinter/page/pkg/egress/socks5/SOCKS5Proxy.command.fs
}

func (px *SOCKS5Proxy) readAddr(r io.Reader) (string, uint16, error) {
// §.splinter/page/pkg/egress/socks5/SOCKS5Proxy.readAddr.fs
}

func (px *SOCKS5Proxy) reply(w io.Writer, rep byte) {
// §.splinter/page/pkg/egress/socks5/SOCKS5Proxy.reply.fs
}

func (px *SOCKS5Proxy) replyBound(w io.Writer, rep byte, host string, port uint16) error {
// §.splinter/page/pkg/egress/socks5/SOCKS5Proxy.replyBound.fs
}

func readByte(r io.Reader, n int) (byte, byte, error) {
// §.splinter/page/pkg/egress/socks5/readByte.fs
}

func readBytes(r io.Reader, n int) ([]byte, error) {
// §.splinter/page/pkg/egress/socks5/readBytes.fs
}

func contains(bs []byte, v byte) bool {
// §.splinter/page/pkg/egress/socks5/contains.fs
}
