// §source page/pkg/egress/policy.go
// Package egress filters the network the way pkg/sandbox filters the
// filesystem.  F12 gave a pod a root it cannot leave; without this it could
// still POST the contents of that root anywhere.  The policy here is the
// decision half — which hosts a confined pod may reach — and the proxies that
// enforce it sit on top of it.
//
// The vocabulary is Anthropic's sandbox-runtime subset, adopted verbatim so a
// policy written for one is readable by the other.
package egress

import (
	"net/netip"
	"strconv"
	"strings"
)

// Policy decides which hosts a sandboxed process may reach.  The zero value
// denies everything: an empty allowlist is no network, never all network, so a
// policy that failed to load cannot silently open the wall.
type Policy struct {
	// AllowedDomains are the only reachable hosts.  Entries are exact
	// hostnames, IP literals, or "*.example.com" wildcards.
	AllowedDomains []string

	// DeniedDomains are checked first and take precedence, so a broad
	// allowlist entry can be carved back without rewriting it.
	DeniedDomains []string

	// AllowLocalBind lets the proxy bind local addresses on the host's
	// behalf.  Allow does not consult it — it is a listener-side switch.
	AllowLocalBind bool
}

// Allow reports whether host may be reached.  The host arrives from the
// client — a proxy request line, a SOCKS5 address field — so it is treated as
// hostile input: canonicalized first, then matched.
func (p *Policy) Allow(host string) bool {
// §.splinter/page/pkg/egress/policy/Policy.Allow.fs
}

// canonical reduces a client-supplied host to the one spelling the matcher
// compares against, and reports false for anything it will not vouch for.
//
// The rejections matter more than the rewrites: a host carrying a control
// character or a NUL passes a naive suffix test here and is then truncated at
// the libc DNS layer, so "evil.test\x00.example.com" would resolve as
// "evil.test" after matching as a subdomain of example.com.
func canonical(host string) (string, bool) {
// §.splinter/page/pkg/egress/policy/canonical.fs
}

// stripPort removes a trailing ":port" and the brackets around an IPv6
// literal.  A bare "::1" has colons but no port, so a colon alone is not
// enough to split on.
func stripPort(host string) string {
// §.splinter/page/pkg/egress/policy/stripPort.fs
}

// parseIP accepts the IP spellings a resolver accepts, not just the tidy one.
// netip.ParseAddr rejects "127.1", but inet_aton does not and neither does
// anything that eventually calls it, so a matcher that only knew the tidy form
// would let "127.1" past a rule written against 127.0.0.1.
func parseIP(host string) (netip.Addr, bool) {
// §.splinter/page/pkg/egress/policy/parseIP.fs
}

// parseInetAton implements the legacy dotted forms: 1 to 4 parts, each octal
// (leading zero), hex (0x) or decimal, with the final part absorbing every
// byte the earlier parts did not name.
func parseInetAton(host string) (netip.Addr, bool) {
// §.splinter/page/pkg/egress/policy/parseInetAton.fs
}

func parseIPPart(part string) (uint64, bool) {
// §.splinter/page/pkg/egress/policy/parseIPPart.fs
}

// match compares one policy entry against an already-canonical host.  The
// pattern goes through the same canonicalization, so a rule written as
// "127.1" or "EXAMPLE.com." covers what its author meant.
func match(pattern, host string) bool {
// §.splinter/page/pkg/egress/policy/match.fs
}
