// §source page/pkg/egress/sni.go
package egress

import (
	"encoding/binary"
	"errors"
	"io"
	"net"
)

// SNI interception closes the domain-fronting hole documented in
// docs/specs/f13-egress.md.  The proxy no longer trusts the client-supplied
// CONNECT hostname alone: it peeks at the first bytes the client sends after
// the tunnel opens, and if they are a TLS ClientHello it extracts the SNI
// and runs it through the same Policy that gated the CONNECT.  A ClientHello
// whose SNI names a host the policy does not allow is refused before any
// bytes reach the upstream — the tunnel is closed, not relayed.
//
// This is SNI peeking, not TLS termination: the proxy reads the ClientHello
// to learn the real destination, validates it, then replays the raw bytes
// to the upstream and relays the rest.  No CA, no MITM, no cert generation.
// The one limit SNI peeking cannot close is CDN domain fronting where the
// SNI and CONNECT host agree but the inner HTTP Host header names a different
// host — that needs full TLS termination and is a later rung.

var (
	// ErrNotTLS means the first bytes from the client are not a TLS
	// handshake.  The caller relays without SNI validation — the peeked
	// bytes are returned for replay.
	ErrNotTLS = errors.New("egress: not a TLS connection")

	// ErrNoSNI means the ClientHello has no SNI extension.  The caller
	// may relay without SNI validation (no SNI to check) or deny — the
	// proxies relay, matching the pre-SNI behaviour for plain tunnels.
	ErrNoSNI = errors.New("egress: TLS ClientHello has no SNI")
)

// sniPeekLimit caps how many bytes peekSNI reads while looking for the SNI.
// A ClientHello is typically under 512 bytes; 16 KiB is the TLS record ceiling.
const sniPeekLimit = 16384

// peekSNI reads the first TLS record from conn, extracts the SNI hostname
// from the ClientHello, and returns the raw bytes read (for replay to the
// upstream) alongside the SNI.  If the first bytes are not a TLS handshake
// it returns ErrNotTLS with the peeked bytes — the caller relays without
// SNI validation.  If the ClientHello has no SNI extension it returns
// ErrNoSNI with the peeked bytes.
func peekSNI(conn net.Conn) (peeked []byte, sni string, err error) {
// §.splinter/page/pkg/egress/sni/peekSNI.fs
}

// parseClientHelloSNI extracts the SNI hostname from a TLS ClientHello
// handshake message (the bytes after the 5-byte TLS record header).
func parseClientHelloSNI(body []byte) (string, error) {
// §.splinter/page/pkg/egress/sni/parseClientHelloSNI.fs
}

// findSNIExtension scans the extensions list for the SNI extension (type
// 0x0000) and returns the first host_name entry.
func findSNIExtension(exts []byte) (string, error) {
// §.splinter/page/pkg/egress/sni/findSNIExtension.fs
}

// parseSNIExtension parses the SNI extension data and returns the first
// host_name (name type 0x00) entry.
func parseSNIExtension(data []byte) (string, error) {
// §.splinter/page/pkg/egress/sni/parseSNIExtension.fs
}