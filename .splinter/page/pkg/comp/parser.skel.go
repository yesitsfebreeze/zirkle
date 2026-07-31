// §source page/pkg/comp/parser.go
package comp

import (
	"regexp"
	"strings"
)

// Shard is one parsed .shard file.
type Shard struct {
	Key         string // path relative to comp root
	Name        string
	Kind        string // tool/knowledge/workflow
	Description string
	Purpose     string
	Tags        []string
	Path        string // original file path
	UseWhen     string
	NotWhen     string
	Danger      string
	SideEffects string
	Requires    []string
	Category    string
	Run         string // default recipe name
	HasFM       bool
	Body        string   // markdown body (no frontmatter)
	Justfile    string   // extracted just block content
	Links       []string // @ref links from body
}

// splitFrontmatter splits content into frontmatter and body at the first
// "---" delimiter pair. Returns ("", content) if no frontmatter.
func splitFrontmatter(content string) (frontmatter, body string) {
// §.splinter/page/pkg/comp/parser/splitFrontmatter.fs
}

// parseFrontmatter parses flat YAML frontmatter: key: value and key: [a, b, c].
// No nesting, no complex YAML — just flat key-value pairs and inline lists.
func parseFrontmatter(fm string) map[string]string {
// §.splinter/page/pkg/comp/parser/parseFrontmatter.fs
}

// parseList parses "[a, b, c]" or "a, b, c" into []string.
func parseList(s string) []string {
// §.splinter/page/pkg/comp/parser/parseList.fs
}

// justBlockRe matches the first ```just fenced block.
var justBlockRe = regexp.MustCompile("(?s)```just\n(.*?)```")

// extractJustBlock returns the content of the first ```just fenced block.
func extractJustBlock(body string) string {
// §.splinter/page/pkg/comp/parser/extractJustBlock.fs
}

// linkRe matches @ref links: @name, @dir/name, @?fuzzy
var linkRe = regexp.MustCompile(`@(\?*)([A-Za-z0-9_./-]+[A-Za-z0-9_/-])`)

// scanLinks extracts @ref links from the markdown body.
func scanLinks(body string) []string {
// §.splinter/page/pkg/comp/parser/scanLinks.fs
}

// Parse parses a .shard file. rel is the path relative to the comp root.
func Parse(rel, content string) (*Shard, error) {
// §.splinter/page/pkg/comp/parser/Parse.fs
}
