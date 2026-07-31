// §head page/pkg/comp/parser.go:32-62 splitFrontmatter
// §sig func splitFrontmatter(content string) (frontmatter, body string)
	s := strings.TrimLeft(content, "\n\r")
	if !strings.HasPrefix(s, "---") {
		return "", content
	}
	// Skip opening ---
	rest := s[3:]
	if len(rest) > 0 && rest[0] == '\n' {
		rest = rest[1:]
	} else if len(rest) > 0 && rest[0] == '\r' {
		rest = rest[1:]
		if len(rest) > 0 && rest[0] == '\n' {
			rest = rest[1:]
		}
	}
	// Find closing ---
	idx := strings.Index(rest, "\n---")
	if idx < 0 {
		// Maybe file ends with --- (no newline after)
		if strings.TrimSpace(rest) == "---" {
			return "", ""
		}
		return "", content
	}
	frontmatter = rest[:idx]
	// Skip closing --- and its newline
	after := rest[idx+4:]
	after = strings.TrimLeft(after, "\n\r")
	body = after
	return frontmatter, body
// §foot page/pkg/comp/parser.go splitFrontmatter