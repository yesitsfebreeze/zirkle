// §head page/pkg/comp/parser.go:135-164 Parse
// §sig func Parse(rel, content string) (*Shard, error)
	fm, body := splitFrontmatter(content)
	shard := &Shard{
		Key:  rel,
		Path: rel,
		Body: strings.TrimSpace(body),
	}
	if fm == "" {
		shard.Links = scanLinks(shard.Body)
		shard.Justfile = extractJustBlock(shard.Body)
		return shard, nil
	}
	shard.HasFM = true
	m := parseFrontmatter(fm)
	shard.Name = m["name"]
	shard.Kind = m["kind"]
	shard.Description = m["description"]
	shard.Purpose = m["purpose"]
	shard.Tags = parseList(m["tags"])
	shard.UseWhen = m["use_when"]
	shard.NotWhen = m["not_when"]
	shard.Danger = m["danger"]
	shard.SideEffects = m["side_effects"]
	shard.Requires = parseList(m["requires"])
	shard.Category = m["category"]
	shard.Run = m["run"]
	shard.Justfile = extractJustBlock(shard.Body)
	shard.Links = scanLinks(shard.Body)
	return shard, nil
// §foot page/pkg/comp/parser.go Parse