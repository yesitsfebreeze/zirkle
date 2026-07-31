// §head page/pkg/store/store.go:460-473 scanExecutions
// §sig func scanExecutions(rows *sql.Rows) ([]*Execution, error)
	var out []*Execution
	for rows.Next() {
		var e Execution
		var success, created int64
		if err := rows.Scan(&e.ID, &e.ParentID, &e.Prompt, &e.Summary, &e.Output, &success, &e.Tokens, &e.Model, &created); err != nil {
			return nil, err
		}
		e.Success = success != 0
		e.CreatedAt = time.Unix(created, 0)
		out = append(out, &e)
	}
	return out, rows.Err()
// §foot page/pkg/store/store.go scanExecutions