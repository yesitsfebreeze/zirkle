// §source page/pkg/sandbox/seatbelt.go
package sandbox

import (
	"fmt"
	"strings"
)

// GenerateSBPL emits a Seatbelt (sandbox-exec) profile from Spec. Paths are
// used as-is; the caller resolves them to absolute before calling, so the
// output is a pure function of Spec — testable on any host.
//
// Policy: deny default, read deny-then-allow on tool paths, write-allow only
// the spec's paths, network denied unless Spec.Net. The egress proxy (when
// configured) is a unix socket accessed via file I/O, so it needs no network
// rule — its socket dir is in Tools and gets file-read*.
func GenerateSBPL(s Spec) string {
// §.splinter/page/pkg/sandbox/seatbelt/GenerateSBPL.fs
}