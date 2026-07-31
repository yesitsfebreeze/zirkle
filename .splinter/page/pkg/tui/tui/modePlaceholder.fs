// §head page/pkg/tui/tui.go:234-245 modePlaceholder
// §sig func modePlaceholder(mode inputMode) string
	switch mode {
	case modeSearch:
		return "filter pods…"
	case modeCommand:
		return "command…  (config, tour, …)"
	case modeHelp:
		return "browse keys · r rebind · a rename"
	default:
		return "/ search · : command · ? help · or type a prompt"
	}
// §foot page/pkg/tui/tui.go modePlaceholder