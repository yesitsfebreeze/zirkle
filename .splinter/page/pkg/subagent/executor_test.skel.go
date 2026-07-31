// §source page/pkg/subagent/executor_test.go
package subagent

import (
	"context"
	"os"
	"path/filepath"
	"strings"
	"testing"
	"time"
)

// shim writes an executable stand-in for ssh.  Pod invokes it as
// `shim <host> <remote command>`, so the script drops the host and runs the
// command locally — every layer of the pod path except the network.
func shim(t *testing.T, body string) string {
// §.splinter/page/pkg/subagent/executor_test/shim.fs
}

// recorder is an Executor that captures the Config it was handed.
type recorder struct {
	got    Config
	called bool
}

func (r *recorder) Run(ctx context.Context, cfg Config) (*Result, error) {
// §.splinter/page/pkg/subagent/executor_test/recorder.Run.fs
}

func TestSpawnRoutesToConfigExecutor(t *testing.T) {
// §.splinter/page/pkg/subagent/executor_test/TestSpawnRoutesToConfigExecutor.fs
}

func TestPodRemoteCommandEscapes(t *testing.T) {
// §.splinter/page/pkg/subagent/executor_test/TestPodRemoteCommandEscapes.fs
}

func TestPodDefaults(t *testing.T) {
// §.splinter/page/pkg/subagent/executor_test/TestPodDefaults.fs
}

func TestPodRunNeedsHost(t *testing.T) {
// §.splinter/page/pkg/subagent/executor_test/TestPodRunNeedsHost.fs
}

// TestPodRunEndToEnd drives the real subagent through the pod transport: the
// shim runs the remote command, which re-execs this test binary with
// RELAY_RESULT_STDOUT=1, so the Result comes back on stdout rather than fd 3.
func TestPodRunEndToEnd(t *testing.T) {
// §.splinter/page/pkg/subagent/executor_test/TestPodRunEndToEnd.fs
}

func TestPodRunIgnoresBannerNoise(t *testing.T) {
// §.splinter/page/pkg/subagent/executor_test/TestPodRunIgnoresBannerNoise.fs
}

// A failing subagent exits 1 but still writes its Result; the parent must read
// it rather than report a transport error.
func TestPodRunKeepsResultOnNonZeroExit(t *testing.T) {
// §.splinter/page/pkg/subagent/executor_test/TestPodRunKeepsResultOnNonZeroExit.fs
}

func TestPodRunTransportError(t *testing.T) {
// §.splinter/page/pkg/subagent/executor_test/TestPodRunTransportError.fs
}

func TestPodRunTimesOut(t *testing.T) {
// §.splinter/page/pkg/subagent/executor_test/TestPodRunTimesOut.fs
}

func TestDecodeResultRejectsGarbage(t *testing.T) {
// §.splinter/page/pkg/subagent/executor_test/TestDecodeResultRejectsGarbage.fs
}

func TestWithDefaultsFillsTimeout(t *testing.T) {
// §.splinter/page/pkg/subagent/executor_test/TestWithDefaultsFillsTimeout.fs
}

// resolveBinary must survive a dev hot-reload that unlinked the running
// binary: when the resolved path is gone it materializes a runnable copy
// from the still-open /proc/self/exe inode.
func TestResolveBinaryMaterializesWhenMissing(t *testing.T) {
// §.splinter/page/pkg/subagent/executor_test/TestResolveBinaryMaterializesWhenMissing.fs
}

// A present binary is bind-mounted as-is — no wasteful copy.
func TestResolveBinaryUsesPathWhenPresent(t *testing.T) {
// §.splinter/page/pkg/subagent/executor_test/TestResolveBinaryUsesPathWhenPresent.fs
}

// Under dev hot-reload the binary path can be unlinked mid-flight, so even a
// path that exists at Stat time must be copied out of /proc/self/exe.
func TestResolveBinaryCopiesInDevMode(t *testing.T) {
// §.splinter/page/pkg/subagent/executor_test/TestResolveBinaryCopiesInDevMode.fs
}
