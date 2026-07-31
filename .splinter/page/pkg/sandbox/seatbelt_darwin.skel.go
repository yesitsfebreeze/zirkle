// §source page/pkg/sandbox/seatbelt_darwin.go
//go:build darwin

package sandbox

import (
	"context"
	"errors"
	"fmt"
	"os"
	"os/exec"
	"path/filepath"
	"strings"
	"time"
)

// ApplyLandlock is a no-op on darwin — Landlock is a Linux-specific kernel
// interface. The Seatbelt backend provides equivalent confinement.
func ApplyLandlock(roPaths, rwPaths []string) error {
// §.splinter/page/pkg/sandbox/seatbelt_darwin/ApplyLandlock.fs
}

// seatbeltBackend is the macOS confinement strategy: sandbox-exec with a
// generated SBPL profile. Zero external dependencies — sandbox-exec ships
// with macOS. Apple deprecated it in 10.13; it is still what Claude Code and
// Codex ship on macOS, so the deprecation is a migration risk, not a blocker.
type seatbeltBackend struct{}

// On darwin, Seatbelt is the default backend. init() runs after the var
// initializer in sandbox.go sets bwrapBackend, so this override wins.
func init() {
// §.splinter/page/pkg/sandbox/seatbelt_darwin/init.fs
}

func (seatbeltBackend) Probe() error {
// §.splinter/page/pkg/sandbox/seatbelt_darwin/seatbeltBackend.Probe.fs
}

func (seatbeltBackend) Command(ctx context.Context, s Spec, argv ...string) (*exec.Cmd, error) {
// §.splinter/page/pkg/sandbox/seatbelt_darwin/seatbeltBackend.Command.fs
}