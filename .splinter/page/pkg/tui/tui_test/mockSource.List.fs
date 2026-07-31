// §head page/pkg/tui/tui_test.go:19-24 mockSource.List
// §sig func (m mockSource) List() ([]PodView, error)
	if m.err != nil {
		return nil, m.err
	}
	return m.views, nil
// §foot page/pkg/tui/tui_test.go mockSource.List