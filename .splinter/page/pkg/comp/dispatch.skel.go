// §source page/pkg/comp/dispatch.go
package comp

import (
	"bytes"
	"io"
	"os"
	"os/exec"
	"path/filepath"
	"runtime"
	"strings"
)

func hostTags() map[string]bool {
// §.splinter/page/pkg/comp/dispatch/hostTags.fs
}

func hostPlatform() string {
// §.splinter/page/pkg/comp/dispatch/hostPlatform.fs
}

func isWSL() bool {
// §.splinter/page/pkg/comp/dispatch/isWSL.fs
}

var platformTags = map[string]bool{
	"unix": true, "macos": true, "windows": true, "wsl": true,
}

func PlatformStrip(justfile string) string {
// §.splinter/page/pkg/comp/dispatch/PlatformStrip.fs
}

func Render(template string, vars map[string]string) string {
// §.splinter/page/pkg/comp/dispatch/Render.fs
}

func firstRecipeName(justfile string) string {
// §.splinter/page/pkg/comp/dispatch/firstRecipeName.fs
}

func Dispatch(shard *Shard, vars map[string]string, args []string) (string, int, error) {
// §.splinter/page/pkg/comp/dispatch/Dispatch.fs
}

func WriteShard(compDir, filename, content string) error {
// §.splinter/page/pkg/comp/dispatch/WriteShard.fs
}
