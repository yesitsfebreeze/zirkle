// §head page/pkg/tui/settings.go:82-89 Model.persistTimeline
// §sig func (m *Model) persistTimeline()
	if m.tlSave == nil {
		return
	}
	if err := m.tlSave(m.tl); err != nil {
		m.err = err.Error()
	}
// §foot page/pkg/tui/settings.go Model.persistTimeline