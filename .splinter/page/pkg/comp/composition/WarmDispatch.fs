// §head page/pkg/comp/composition.go:45-64 WarmDispatch
// §sig func WarmDispatch(db *sql.DB, store *Store, query string) (*Shard, string, int, error)
	all, err := store.All()
	if err != nil {
		return nil, "", 0, err
	}
	ranked := Rank(all, query)
	if len(ranked) == 0 {
		return nil, "", 0, nil
	}
	top := &ranked[0]
	if top.Name == "" && top.Description == "" {
		return nil, "", 0, nil
	}
	output, exitCode, err := Dispatch(top, nil, nil)
	if err != nil {
		return top, "", 0, err
	}
	_ = RecordResult(db, top.Key, exitCode == 0)
	return top, output, exitCode, nil
// §foot page/pkg/comp/composition.go WarmDispatch