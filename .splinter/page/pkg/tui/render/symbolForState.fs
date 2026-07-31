// §head page/pkg/tui/render.go:117-132 symbolForState
// §sig func symbolForState(state string) string
	switch state {
	case "done", "stopped":
		return "■"
	case "running":
		return "▶"
	case "created", "waiting", "planning", "approved":
		return "●"
	case "failed", "stuck":
		return "✕"
	case "ready":
		return "+"
	default:
		return "●"
	}
// §foot page/pkg/tui/render.go symbolForState