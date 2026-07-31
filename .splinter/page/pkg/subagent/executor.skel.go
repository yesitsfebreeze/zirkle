// §source page/pkg/subagent/executor.go
package subagent

import (
	"context"
	"encoding/json"
	"errors"
	"fmt"
	"io"
	"os"
	"os/exec"
	"path/filepath"
	"strconv"
	"strings"
	"time"

	"github.com/feb/relay/pkg/fault"
	"github.com/feb/relay/pkg/hotreload"
	"github.com/feb/relay/pkg/sandbox"
)

// Executor decides where a subagent runs.  Local re-execs this binary as a
// child process; Pod runs the same binary on a remote machine.  Config.Executor
// selects one, and nil means Local, so callers that never heard of executors
// keep their old behaviour.
type Executor interface {
	Run(ctx context.Context, cfg Config) (*Result, error)
}

// Local runs the subagent as a child of this process, handing it a pipe on
// fd 3 to write its Result to.
type Local struct{}

// Run spawns the child, waits for the Result JSON on fd 3, and kills the child
// if the timeout expires.
func (Local) Run(ctx context.Context, cfg Config) (*Result, error) {
// §.splinter/page/pkg/subagent/executor/Local.Run.fs
}

// Pod runs the subagent on a remote machine — a pod — over ssh.  The pod is
// an ordinary host with the same static binary on it: no image, no runtime, no
// package manager, so bootstrapping one is a file copy.
type Pod struct {
	Host    string   // user@host — the pod
	Binary  string   // remote binary path (default "relay")
	Command string   // transport command (default "ssh")
	Args    []string // extra transport args, placed before Host
	Env     []string // KEY=VALUE pairs exported on the pod
}

// Run executes the subagent on the pod and decodes its Result from stdout.
// fd 3 does not cross an ssh session, so the remote command sets
// RELAY_RESULT_STDOUT=1 and the subagent writes its Result to stdout instead;
// stderr stays its log channel.
func (o Pod) Run(ctx context.Context, cfg Config) (*Result, error) {
// §.splinter/page/pkg/subagent/executor/Pod.Run.fs
}

// Sandboxed runs the subagent on this host but inside a filesystem it cannot
// leave.  Same binary, same protocol as Pod — only the boundary differs: Pod
// puts a machine between the agent and the host, Sandboxed puts a mount
// namespace there.
type Sandboxed struct {
	Spec   sandbox.Spec
	Binary string   // host path to the relay binary (default os.Args[0])
	Env    []string // KEY=VALUE pairs added to the sandbox environment
}

// Run mounts the binary read-only into the sandbox and executes it there.  The
// sandbox has no fd 3 to write to, so the Result comes back over stdout on the
// same path Pod uses.
func (s Sandboxed) Run(ctx context.Context, cfg Config) (*Result, error) {
// §.splinter/page/pkg/subagent/executor/Sandboxed.Run.fs
}

func (o Pod) command() string {
// §.splinter/page/pkg/subagent/executor/Pod.command.fs
}

func (o Pod) binary() string {
// §.splinter/page/pkg/subagent/executor/Pod.binary.fs
}

// remoteCommand builds the single shell word ssh runs on the pod.  Every
// interpolated value is single-quote escaped; the pod never inherits the
// parent's environment, so credentials travel through Pod.Env.
func (o Pod) remoteCommand(cfg Config) string {
// §.splinter/page/pkg/subagent/executor/Pod.remoteCommand.fs
}

// subagentArgs is the argument list that turns the binary into a subagent.
// Both executors send the same flags; only the transport differs.
func subagentArgs(cfg Config) []string {
// §.splinter/page/pkg/subagent/executor/subagentArgs.fs
}

// timedOut is the Result a parent gets when the subagent outlived its
// deadline — the loop continues without it rather than failing.
func timedOut(cfg Config) *Result {
// §.splinter/page/pkg/subagent/executor/timedOut.fs
}

// decodeResult pulls the Result out of a remote stdout stream, scanning from
// the end so ssh banners and MOTD noise ahead of it are ignored.
func decodeResult(out []byte) (*Result, error) {
// §.splinter/page/pkg/subagent/executor/decodeResult.fs
}

// shellQuote wraps a value in single quotes for the remote shell.
func shellQuote(s string) string {
// §.splinter/page/pkg/subagent/executor/shellQuote.fs
}

// resolveBinary returns a host path to the relay binary that the sandbox can
// bind-mount, plus a cleanup func. When want is empty it uses the running
// binary. Under dev hot-reload the running binary is rebuilt in place, so its
// path is unlinked (/proc/self/exe shows a " (deleted)" suffix) and a plain
// bind-mount fails with "execvp ...: No such file or directory". In that case
// it materializes a copy from the still-open /proc/self/exe inode, which the
// kernel keeps alive for as long as this process runs.
func resolveBinary(want string) (string, func(), error) {
// §.splinter/page/pkg/subagent/executor/resolveBinary.fs
}

// withDefaults fills the zero values an executor relies on.
func (c Config) withDefaults() Config {
// §.splinter/page/pkg/subagent/executor/Config.withDefaults.fs
}
