// §source page/pkg/sandbox/egress.go
package sandbox

import (
	"os"
	"path/filepath"

	"github.com/feb/relay/pkg/egress"
)

// StartEgress starts HTTP and SOCKS5 proxies configured with policy, injects
// proxy env vars into spec, and adds the socket dir to spec.Tools so bwrap
// bind-mounts it read-only.  Returns a cleanup function that shuts down the
// proxies and removes the temp dir.
//
// The proxies run in goroutines and stop when their listeners are closed.
// Call cleanup exactly once (typically via defer).
func StartEgress(spec *Spec, policy *egress.Policy) (func(), error) {
// §.splinter/page/pkg/sandbox/egress/StartEgress.fs
}
