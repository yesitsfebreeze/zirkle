// §head page/pkg/comp/store.go:154-174 scanShards
// §sig func scanShards(rows *sql.Rows) ([]Shard, error)
	var out []Shard
	for rows.Next() {
		var shard Shard
		var tags, requires string
		var hasFM int
		if err := rows.Scan(
			&shard.Key, &shard.Name, &shard.Kind, &shard.Description, &shard.Purpose,
			&tags, &shard.Path, &shard.UseWhen, &shard.NotWhen, &shard.Danger,
			&shard.SideEffects, &requires, &shard.Category, &shard.Run, &hasFM,
			&shard.Body, &shard.Justfile,
		); err != nil {
			return nil, err
		}
		shard.Tags = splitCSV(tags)
		shard.Requires = splitCSV(requires)
		shard.HasFM = intToBool(hasFM)
		out = append(out, shard)
	}
	return out, rows.Err()
// §foot page/pkg/comp/store.go scanShards