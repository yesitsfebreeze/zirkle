// §head page/pkg/tui/tui.go:172-183 promptIcon
// §sig func promptIcon(m inputMode) string
	switch m {
	case modeSearch:
		return "/"
	case modeCommand:
		return ":"
	case modeHelp:
		return "?"
	default:
		return ">"
	}
// §foot page/pkg/tui/tui.go promptIcon