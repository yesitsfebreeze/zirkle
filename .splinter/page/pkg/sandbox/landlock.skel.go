// §source page/pkg/sandbox/landlock.go
//go:build linux

package sandbox

import (
	"fmt"
	"unsafe"

	"golang.org/x/sys/unix"
)

// LandlockABI returns the highest Landlock ABI version the kernel supports,
// or 0 if Landlock is unavailable. ABI 1 = filesystem, ABI 2 = truncate,
// ABI 3 = refer, ABI 4 = net port, ABI 5 = ioctl_dev.
func LandlockABI() int {
// §.splinter/page/pkg/sandbox/landlock/LandlockABI.fs
}

// landlockRulesetAttr is the ruleset attribute struct for landlock_create_ruleset.
type landlockRulesetAttr struct {
	HandledAccessFS  uint64
	HandledAccessNet uint64 // ABI 4+
}

// landlockPathBeneathAttr is the path-beneath rule attribute struct.
type landlockPathBeneathAttr struct {
	AllowedAccess uint64
	ParentFD      int32
}

// ApplyLandlock creates a ruleset, adds read-only rules for roPaths and
// read-write rules for rwPaths, then restricts the current thread. Returns nil
// if Landlock is unavailable (not fatal — bwrap is still the boundary). Must
// be called with the OS thread locked (runtime.LockOSThread) so the restriction
// sticks to the calling thread.
func ApplyLandlock(roPaths, rwPaths []string) error {
// §.splinter/page/pkg/sandbox/landlock/ApplyLandlock.fs
}

func addLandlockRule(rulesetFD int, path string, access uint64) error {
// §.splinter/page/pkg/sandbox/landlock/addLandlockRule.fs
}
