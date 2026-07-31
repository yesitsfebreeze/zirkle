// §head page/pkg/comp/dispatch.go:94-100 Render
// §sig func Render(template string, vars map[string]string) string
	out := template
	for k, v := range vars {
		out = strings.ReplaceAll(out, "<<"+k+">>", v)
	}
	return out
// §foot page/pkg/comp/dispatch.go Render