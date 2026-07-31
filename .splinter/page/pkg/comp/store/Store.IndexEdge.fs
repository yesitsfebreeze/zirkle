// §head page/pkg/comp/store.go:75-78 Store.IndexEdge
// §sig func (s *Store) IndexEdge(src, dst string) error
	_, err := s.db.Exec(`INSERT OR IGNORE INTO edge (src, dst) VALUES (?, ?)`, src, dst)
	return err
// §foot page/pkg/comp/store.go Store.IndexEdge