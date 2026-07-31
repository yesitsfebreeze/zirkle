// §head page/pkg/tui/tui.go:248-264 New
// §sig func New(src Source, cmdr Commander, broadcasts <-chan string) Model
	mi := NewMultiInput(60)
	mi.ta.Placeholder = "dispatch a job…"
	mi.ta.MaxHeight = 3
	m := Model{
		src: src, cmdr: cmdr, broadcasts: broadcasts,
		input: mi, collapsed: map[int]bool{},
		vp: viewport.New(80, 10), vpTerminal: viewport.New(40, 10), vpChat: viewport.New(40, 10),
		detail: "", histIdx: -1, search: false, mode: modeNormal,
		highlightColor: ansiMagenta,
		attentionColor: ansiBlue,
		km:             keymap.New(),
		tl:             DefaultTimeline(),
	}
	m.updateColors(ansiMagenta, ansiBlue)
	return m
// §foot page/pkg/tui/tui.go New