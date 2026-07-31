// §head page/pkg/tui/settings.go:49-63 Model.toggleSetting
// §sig func (m *Model) toggleSetting(i int)
	switch i {
	case 2:
		m.tl.Enabled = !m.tl.Enabled
	case 3:
		m.tl.ShowCount = !m.tl.ShowCount
	case 4:
		m.tl.ShowStates = !m.tl.ShowStates
	case 5:
		m.tl.ShowSpan = !m.tl.ShowSpan
	default:
		return
	}
	m.persistTimeline()
// §foot page/pkg/tui/settings.go Model.toggleSetting