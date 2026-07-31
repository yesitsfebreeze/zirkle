// §source page/pkg/sandbox/sandbox.go
// Package sandbox confines a process to one directory.  The pod gets a
// filesystem it cannot leave: host tools are bind-mounted read-only, one
// writable root holds its work, and nothing else is mounted at all — an escape
// is not denied by policy, it simply has nowhere to land.
package sandbox

import (
	"context"
	"errors"
	"fmt"
	"os"
	"os/exec"
	"path/filepath"
	"strconv"
	"strings"
	"time"

	"github.com/feb/relay/pkg/egress"
)

// Root is where the sandboxed process sees its own writable world, whatever
// the host path behind it is.
const Root = "/pod"

// waitDelay bounds how long a finished sandbox may hold its stdio open.
const waitDelay = time.Second

// Spec describes one sandbox.  The zero value is not usable: Dir must name a
// host directory, since a sandbox with no writable root has nothing to run in.
type Spec struct {
	// Dir is the host directory backing Root.  Ephemeral ignores it as a
	// data source but still needs it as the mount point's origin.
	Dir string

	// Ephemeral makes Root a tmpfs sized SizeMB — RAM-backed, capped, and
	// gone when the process exits.  Otherwise Dir is bind-mounted, so the
	// pod's work survives it.
	Ephemeral bool

	// SizeMB caps the tmpfs mounts.  Zero means 256 MiB.
	SizeMB int

	// Tools are host paths bind-mounted read-only, the pod's toolchain.
	// Empty means DefaultTools.
	Tools []string

	// RW are extra host paths the pod may write to, mounted at the same
	// path inside.  Every one of them is a hole in the wall, so the list is
	// the audit surface: short by default, explicit when it grows.
	RW []string

	// Net leaves the network namespace shared with the host.  Off by
	// default: a pod that cannot write outside its root but can still POST
	// the contents of its root somewhere is not contained.
	// When Net is false and Egress is set, the sandbox gets an empty netns
	// plus host-side HTTP/SOCKS5 proxies on unix sockets bind-mounted in,
	// so the pod can reach only the hosts the egress policy allows.
	Net bool

	// SharePID leaves the PID namespace shared with the host.  Off by
	// default — a confined pod should not see or signal host processes.
	// A worker that manages the host (listing/killing processes) needs this.
	SharePID bool

	// Egress is the network policy enforced by host-side proxies.  When
	// non-nil the sandbox starts HTTP and SOCKS5 proxies on unix sockets,
	// bind-mounts the socket dir and sets HTTP_PROXY/HTTPS_PROXY/ALL_PROXY.
	// Nil means no proxy infrastructure: Net controls the boundary alone.
	Egress *egress.Policy

	// Hostname the pod sees.  Empty means "pod".
	Hostname string

	// Env is the sandboxed process's entire environment.  The host
	// environment is cleared first, so nothing leaks in unlisted.
	Env []string
}

// DefaultTools is the read-only host surface a pod needs to run ordinary
// programs: the merged-usr tree plus enough of /etc to resolve users and CAs.
var DefaultTools = []string{
	"/usr",
	"/etc/alternatives",
	"/etc/ssl",
	"/etc/ca-certificates",
	"/etc/passwd",
	"/etc/group",
}

// DefaultEnv is what a sandbox gets when Spec.Env is nil.  The host
// environment is never inherited, so without this nothing on PATH resolves.
var DefaultEnv = []string{
	"PATH=/usr/bin:/bin:/usr/sbin:/sbin",
	"HOME=" + Root,
	"TMPDIR=/tmp",
}

// Backend is the confinement strategy: bwrap on Linux, sandbox-exec on
// macOS, Landlock as a second layer.  Probe selects; Command builds the
// confined process.
type Backend interface {
	Probe() error
	Command(ctx context.Context, s Spec, argv ...string) (*exec.Cmd, error)
}

// ErrUnavailable means this backend cannot run on the current host.
var ErrUnavailable = errors.New("sandbox unavailable")

// activeBackend is the backend selected at Probe time.  Defaults to bwrap;
// future backends (Landlock, Seatbelt) register here.
var activeBackend Backend = bwrapBackend{}

// Probe reports whether this host can build a sandbox, naming the fix when it
// cannot.  It runs a real sandbox through the same argv builder every pod uses,
// so a policy that would fail in production cannot pass the probe.
func Probe() error {
// §.splinter/page/pkg/sandbox/sandbox/Probe.fs
}

// Command builds the confined command.  argv is resolved inside the sandbox,
// not on the host, so it must name a path the sandbox can see.
func (s Spec) Command(ctx context.Context, argv ...string) (*exec.Cmd, error) {
// §.splinter/page/pkg/sandbox/sandbox/Spec.Command.fs
}

// bwrapBackend is the Linux bubblewrap confinement strategy.
type bwrapBackend struct{}

func (bwrapBackend) Probe() error {
// §.splinter/page/pkg/sandbox/sandbox/bwrapBackend.Probe.fs
}

func (bwrapBackend) Command(ctx context.Context, s Spec, argv ...string) (*exec.Cmd, error) {
// §.splinter/page/pkg/sandbox/sandbox/bwrapBackend.Command.fs
}

// bwrapArgs is the whole confinement policy in one list.  Read it top to
// bottom and you have read the security model.
func (s Spec) bwrapArgs(dir string) ([]string, error) {
// §.splinter/page/pkg/sandbox/sandbox/Spec.bwrapArgs.fs
}
