// §head page/pkg/subagent/executor.go:319-321 shellQuote
// §sig func shellQuote(s string) string
	return "'" + strings.ReplaceAll(s, "'", `'\''`) + "'"
// §foot page/pkg/subagent/executor.go shellQuote