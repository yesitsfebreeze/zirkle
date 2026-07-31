// §source page/pkg/comp/store.go
package comp

import (
	"database/sql"
	"strings"
)

// Store is the SQLite-backed shard index. It shares the pod database
// connection — shard and edge tables sit alongside the pod tables.
type Store struct {
	db *sql.DB
}

// Open wraps an existing database connection.
func Open(db *sql.DB) *Store {
// §.splinter/page/pkg/comp/store/Open.fs
}

// EnsureSchema creates the shard, edge, and shard_rating tables if they do not exist.
func (s *Store) EnsureSchema() error {
// §.splinter/page/pkg/comp/store/Store.EnsureSchema.fs
}

// Index inserts or replaces a shard in the database.
func (s *Store) Index(shard *Shard) error {
// §.splinter/page/pkg/comp/store/Store.Index.fs
}

// IndexEdge records a link between two shards.
func (s *Store) IndexEdge(src, dst string) error {
// §.splinter/page/pkg/comp/store/Store.IndexEdge.fs
}

// All returns every indexed shard.
func (s *Store) All() ([]Shard, error) {
// §.splinter/page/pkg/comp/store/Store.All.fs
}

// Get returns one shard by key, or sql.ErrNoRows.
func (s *Store) Get(key string) (*Shard, error) {
// §.splinter/page/pkg/comp/store/Store.Get.fs
}

// Search does a LIKE query across name, description, tags, use_when.
// Rank refines the results with field weighting.
func (s *Store) Search(query string) ([]Shard, error) {
// §.splinter/page/pkg/comp/store/Store.Search.fs
}

func (s *Store) DB() *sql.DB {
// §.splinter/page/pkg/comp/store/Store.DB.fs
}

func boolToInt(b bool) int {
// §.splinter/page/pkg/comp/store/boolToInt.fs
}

func intToBool(i int) bool {
// §.splinter/page/pkg/comp/store/intToBool.fs
}

func scanShard(row interface{ Scan(...any) error }) (*Shard, error) {
// §.splinter/page/pkg/comp/store/scanShard.fs
}

func scanShards(rows *sql.Rows) ([]Shard, error) {
// §.splinter/page/pkg/comp/store/scanShards.fs
}

func splitCSV(s string) []string {
// §.splinter/page/pkg/comp/store/splitCSV.fs
}
