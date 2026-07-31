// §source page/pkg/config/config_test.go
package config

import (
	"os"
	"path/filepath"
	"reflect"
	"testing"

	"github.com/feb/relay/pkg/egress"
)

func writeConfigFile(t *testing.T, body string) string {
// §.splinter/page/pkg/config/config_test/writeConfigFile.fs
}

func TestDefaultConfig(t *testing.T) {
// §.splinter/page/pkg/config/config_test/TestDefaultConfig.fs
}

func TestLoadMissingFile(t *testing.T) {
// §.splinter/page/pkg/config/config_test/TestLoadMissingFile.fs
}

func TestLoadEmptyPath(t *testing.T) {
// §.splinter/page/pkg/config/config_test/TestLoadEmptyPath.fs
}

func TestConfigFileOverridesDefaults(t *testing.T) {
// §.splinter/page/pkg/config/config_test/TestConfigFileOverridesDefaults.fs
}

func TestEnvOverridesConfigFile(t *testing.T) {
// §.splinter/page/pkg/config/config_test/TestEnvOverridesConfigFile.fs
}

func TestEnvOverridesDefaults(t *testing.T) {
// §.splinter/page/pkg/config/config_test/TestEnvOverridesDefaults.fs
}

func TestStoreDirTildeExpansion(t *testing.T) {
// §.splinter/page/pkg/config/config_test/TestStoreDirTildeExpansion.fs
}

func TestEgressPolicyFromConfig(t *testing.T) {
// §.splinter/page/pkg/config/config_test/TestEgressPolicyFromConfig.fs
}

func TestPartialConfigFile(t *testing.T) {
// §.splinter/page/pkg/config/config_test/TestPartialConfigFile.fs
}

// Internal: egress.Policy import compiles and is usable.
var _ *egress.Policy = (&Config{}).EgressPolicy()
func TestThemeCustomParsed(t *testing.T) {
// §.splinter/page/pkg/config/config_test/TestThemeCustomParsed.fs
}

// Default config ships the designed palette as truecolour (custom=true), so
// relay renders purple-on-near-black without depending on the terminal's ANSI
// row. Flip custom=false in ~/.relay/config.toml to defer to the terminal.
func TestThemeDefaultIsDesignedPalette(t *testing.T) {
// §.splinter/page/pkg/config/config_test/TestThemeDefaultIsDesignedPalette.fs
}

// EnsureDefault writes the embedded file when none exists, leaves it alone
// when one does.
func TestEnsureDefaultCreatesThenPreserves(t *testing.T) {
// §.splinter/page/pkg/config/config_test/TestEnsureDefaultCreatesThenPreserves.fs
}
