// §source page/pkg/sandbox/landlock_test.go
package sandbox

import (
	"os"
	"os/exec"
	"path/filepath"
	"runtime"
	"testing"

	"golang.org/x/sys/unix"
)

func TestLandlockABI(t *testing.T) {
// §.splinter/page/pkg/sandbox/landlock_test/TestLandlockABI.fs
}

func TestLandlockDeniesWrite(t *testing.T) {
// §.splinter/page/pkg/sandbox/landlock_test/TestLandlockDeniesWrite.fs
}
