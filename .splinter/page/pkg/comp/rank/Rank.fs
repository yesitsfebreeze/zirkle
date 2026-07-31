// §head page/pkg/comp/rank.go:45-89 Rank
// §sig func Rank(rows []Shard, query string) []Shard
	terms := tokenize(query)
	if len(terms) == 0 {
		return rows
	}

	scored := make([]scoredShard, len(rows))
	for i := range rows {
		s := &rows[i]
		var score int

		if containsAny(s.Name, terms) {
			score += 3
		}
		if containsAny(s.UseWhen, terms) {
			score += 3
		}
		for _, tag := range s.Tags {
			if containsAny(tag, terms) {
				score += 2
				break
			}
		}
		if containsAny(s.Description, terms) {
			score += 1
		}

		// not_when veto
		if containsAny(s.NotWhen, terms) {
			score = 0
		}

		scored[i] = scoredShard{shard: &rows[i], score: score}
	}

	sort.SliceStable(scored, func(i, j int) bool {
		return scored[i].score > scored[j].score
	})

	out := make([]Shard, len(scored))
	for i := range scored {
		out[i] = *scored[i].shard
	}
	return out
// §foot page/pkg/comp/rank.go Rank