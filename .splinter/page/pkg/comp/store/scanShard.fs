// §head page/pkg/comp/store.go:135-152 scanShard
// §sig func scanShard(row interface{ Scan(...any) error }) (*Shard, error)
	var shard Shard
	var tags, requires string
	var hasFM int
	err := row.Scan(
		&shard.Key, &shard.Name, &shard.Kind, &shard.Description, &shard.Purpose,
		&tags, &shard.Path, &shard.UseWhen, &shard.NotWhen, &shard.Danger,
		&shard.SideEffects, &requires, &shard.Category, &shard.Run, &hasFM,
		&shard.Body, &shard.Justfile,
	)
	if err != nil {
		return nil, err
	}
	shard.Tags = splitCSV(tags)
	shard.Requires = splitCSV(requires)
	shard.HasFM = intToBool(hasFM)
	return &shard, nil
// §foot page/pkg/comp/store.go scanShard