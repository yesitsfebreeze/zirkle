// §source page/pkg/hotreload/hotreload.go
package hotreload

import (
	"context"
	"fmt"
	"io/fs"
	"os"
	"os/exec"
	"os/signal"
	"path/filepath"
	"runtime"
	"strings"
	"sync"
	"syscall"
	"time"
)

// Config configures file watching and supervision.
type Config struct {
	RootDir        string
	WatchExts      []string
	IgnoredDirs    []string
	PollInterval   time.Duration
	DebounceWindow time.Duration
}

// Watcher monitors file modification times recursively.
type Watcher struct {
	cfg    Config
	mu     sync.Mutex
	mtimes map[string]time.Time
}

// NewWatcher initializes a file watcher with default options if unset.
func NewWatcher(cfg Config) *Watcher {
// §.splinter/page/pkg/hotreload/hotreload/NewWatcher.fs
}

// Scan checks for modified, added, or deleted files under RootDir.
func (w *Watcher) Scan() bool {
// §.splinter/page/pkg/hotreload/hotreload/Watcher.Scan.fs
}

// Watch continuously scans for changes until context cancellation.
func (w *Watcher) Watch(ctx context.Context, events chan<- struct{}) {
// §.splinter/page/pkg/hotreload/hotreload/Watcher.Watch.fs
}

// IsDevChild returns true if current process is child spawned by hot reload.
func IsDevChild() bool {
// §.splinter/page/pkg/hotreload/hotreload/IsDevChild.fs
}

// IsDevMode returns true if running in dev mode.
func IsDevMode() bool {
// §.splinter/page/pkg/hotreload/hotreload/IsDevMode.fs
}

// Supervise watches files, compiles temporary binary, and manages child process.
func Supervise(args []string) error {
// §.splinter/page/pkg/hotreload/hotreload/Supervise.fs
}

func findGoBinary() string {
// §.splinter/page/pkg/hotreload/hotreload/findGoBinary.fs
}

func buildBinary(goBin, outputPath string) error {
// §.splinter/page/pkg/hotreload/hotreload/buildBinary.fs
}
