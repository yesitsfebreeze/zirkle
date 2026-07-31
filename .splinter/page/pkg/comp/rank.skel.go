// §source page/pkg/comp/rank.go
package comp

import (
	"sort"
	"strings"
)

var stopwords = map[string]bool{
	"a": true, "an": true, "the": true, "is": true, "are": true,
	"was": true, "were": true, "to": true, "for": true, "of": true,
	"in": true, "on": true, "at": true, "by": true, "with": true,
	"from": true, "and": true, "or": true, "not": true, "but": true,
	"if": true, "when": true, "how": true, "what": true, "why": true,
	"who": true, "do": true, "does": true, "did": true, "can": true,
	"could": true, "should": true, "would": true, "will": true, "be": true,
	"been": true, "have": true, "has": true, "had": true, "this": true,
	"that": true, "these": true, "those": true, "it": true, "its": true,
	"as": true, "up": true, "out": true, "no": true, "yes": true,
}

func tokenize(query string) []string {
// §.splinter/page/pkg/comp/rank/tokenize.fs
}

func containsAny(s string, terms []string) bool {
// §.splinter/page/pkg/comp/rank/containsAny.fs
}

// Rank scores shards against a query using field weighting.
// name/use_when: +3 per term match, tags: +2, description: +1.
// not_when veto: if query matches not_when, score = 0.
// Returns sorted desc by score.
func Rank(rows []Shard, query string) []Shard {
// §.splinter/page/pkg/comp/rank/Rank.fs
}

type scoredShard struct {
	shard *Shard
	score int
}
