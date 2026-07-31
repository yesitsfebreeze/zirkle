// §head page/pkg/tui/settings.go:66-77 Model.cycleFrame
// §sig func (m *Model) cycleFrame(dir int)
	cur := 0
	for i, f := range frameOptions {
		if f == m.tl.Frame {
			cur = i
			break
		}
	}
	next := (cur + dir + len(frameOptions)) % len(frameOptions)
	m.tl.Frame = frameOptions[next]
	m.persistTimeline()
// §foot page/pkg/tui/settings.go Model.cycleFrame