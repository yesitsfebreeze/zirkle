// §source page/pkg/comp/composition_test.go
package comp

import (
	"database/sql"
	"os"
	"path/filepath"
	"testing"

	_ "modernc.org/sqlite"
)

func TestLoadComp(t *testing.T) {
// §.splinter/page/pkg/comp/composition_test/TestLoadComp.fs
}

func TestLoadCompNoShardsDir(t *testing.T) {
// §.splinter/page/pkg/comp/composition_test/TestLoadCompNoShardsDir.fs
}

func TestLoadCompSkipsNonShard(t *testing.T) {
// §.splinter/page/pkg/comp/composition_test/TestLoadCompSkipsNonShard.fs
}
