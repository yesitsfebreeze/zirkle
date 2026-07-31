// §source page/pkg/comp/composition.go
package comp

import (
	"database/sql"
	"os"
	"path/filepath"
	"strings"
)

type Composition struct {
	Root  string
	Store *Store
}

func LoadComp(root string, store *Store) (*Composition, error) {
// §.splinter/page/pkg/comp/composition/LoadComp.fs
}

func WarmDispatch(db *sql.DB, store *Store, query string) (*Shard, string, int, error) {
// §.splinter/page/pkg/comp/composition/WarmDispatch.fs
}
